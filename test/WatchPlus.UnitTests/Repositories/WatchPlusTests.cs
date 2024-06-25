
using WatchPlus.Models;
using WatchPlus.Repositories.Base;

namespace WatchPlus.UnitTests;

public class WatchPlusTests
{
    [Fact]
    public async Task GetUserAsync_UserNotFound_ThrowsNotFoundException()
    {
        Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();

        userRepositoryMock.Setup((repo) => repo.FindByName("test"))
            .ReturnsAsync(value: null);

        var userService = new UserService(
            dbContext: null,
            userRepository: userRepositoryMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(async () => await userService.GetUserAsync("test"));
    }


}