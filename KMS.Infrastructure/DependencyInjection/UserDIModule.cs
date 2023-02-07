using Autofac;
using KMS.Core.Interfaces.Role;
using KMS.Core.Interfaces.User;
using KMS.Core.Services.Role;
using KMS.Core.Services.User;

namespace KMS.Infrastructure.DependencyInjection;

public class UserDIModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserService>()
            .As<IUserService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<RoleService>()
            .As<IRoleService>()
            .InstancePerLifetimeScope();
    }
}
