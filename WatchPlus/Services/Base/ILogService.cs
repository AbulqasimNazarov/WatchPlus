using WatchPlus.Models;

namespace WatchPlus.Services.Base;

public interface ILogService
{
     public Task CreateNewLogAsync(Log newLog);
}
