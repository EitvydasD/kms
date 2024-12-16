using KMS.Core.Aggregates.Role;
using KMS.Core.Aggregates.Role.Entities;
using KMS.Core.Aggregates.User.Entities;
using KMS.Infrastructure.Data;

namespace KMS.Infrastructure;
public class DatabaseSeeder
{
    public DatabaseSeeder(DatabaseContext context)
    {
        _context = context;
    }

    private readonly DatabaseContext _context;

    public void Seed()
    {
        SeedRoles();
        SeedUsers();
    }

    private void SeedRoles()
    {
        var role = _context.Roles.FirstOrDefault(x => x.Name == "Full_Access");

        if (role is null)
        {
            var permissions = Enum.GetValues(typeof(PermissionId))
                .Cast<PermissionId>()
                .Select(x => x.ToString())
                .ToList();

            var newRole = new RoleEntity
            {
                Name = "Full_Access",
                IsDefault = false,
            };

            newRole.SetPermissions(permissions);

            _context.Roles.AddAsync(newRole);
        }
    }

    private void SeedUsers()
    {
        var user = _context.Users.FirstOrDefault(x => x.Username == "admin");

        if (user is null)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Name == "Full_Access");

            var newUser = new UserEntity
            {
                Username = "admin",
                FirstName = "Admin",
                LastName = "Admin",
                Phone = "+37060000000",
                Email = "admin@admin.admin",
                BirthDate = DateOnly.Parse("1990-01-01"),
            };

            if (role is not null)
            {
                newUser.Roles.Add(new UserRoleEntity
                {
                    RoleId = role.Id,
                    UserId = newUser.Id,
                });
            }

            newUser.ChangePassword("admin");

            _context.Users.AddAsync(newUser);
        }
    }
}
