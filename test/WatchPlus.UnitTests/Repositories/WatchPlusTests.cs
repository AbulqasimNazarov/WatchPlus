
using WatchPlus.Models;
using WatchPlus.Services;
using WatchPlus.Repositories;
using WatchPlus.Repositories.Base;
using Moq;


namespace WatchPlus.UnitTests;

public class WatchPlusTests
{
    [Fact]
    public async Task GetUserAsync_UserNotFound_ThrowsNullException()
    {
        Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        var guidId = new Guid("a53cccbc-26a8-491e-aabd-39f035bf9dd0");

        userRepositoryMock.Setup((repo) => repo.GetByIdAsync(guidId))
            .ReturnsAsync(value: null);

        var userService = new UserService(
            userRepository: userRepositoryMock.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.GetUserByIdAsync(guidId));
    }


}