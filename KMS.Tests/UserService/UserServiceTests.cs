using KMS.Core.Aggregates.User.Entities;
using KMS.Core.Interfaces.Comment;
using KMS.Core.Interfaces.User;
using Moq;
using Utils.Library.Interfaces;

namespace KMS.Tests.UserService;
public class UserServiceTests
{
    #region Data

    protected UserEntity AdminUser => new()
    {
        Id = new Guid("406AD398-D14C-4C38-81CC-09D835B3EE1F"),
        Username = "admin",
        Email = "admin@admin.admin",
        FirstName = "The name of the Admin",
        LastName = "The last name of the Admin",
        Phone = "1234567890",
    };

    protected UserEntity User => new()
    {
        Id = new Guid("406AD398-D14C-4C38-81CC-09D835B3EE1B"),
        Username = "user",
        Email = "user@user.user",
        FirstName = "The name of the User",
        LastName = "The last name of the User",
        Phone = "1234567890",
    };

    #endregion
    protected IUserService MockServiceWithData(params UserEntity[] userEntities)
    {
        var mockRepo = new Mock<IRepository<UserEntity>>();
        foreach (var user in userEntities)
        {
            mockRepo
                    .Setup(repo => repo.GetByIdAsync(user.Id, CancellationToken.None))
                    .ReturnsAsync(user);
        }

        return new Core.Services.User.UserService(mockRepo.Object, new Mock<ICommentService>().Object);
    }
}
