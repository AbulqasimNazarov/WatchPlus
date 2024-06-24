using WatchPlus.Dtos;
using WatchPlus.Models;

namespace WatchPlus.Repositories.Base;

public interface IUserRepository : ICreatableAsync<User>, IGetableUser<User>, IUpdateUser<User>
{
    
    
}
