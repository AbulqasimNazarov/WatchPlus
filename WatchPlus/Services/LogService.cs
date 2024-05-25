using WatchPlus.Models;
using WatchPlus.Repositories.Base;
using WatchPlus.Services.Base;

namespace WatchPlus.Services;

public class LogService : ILogService
{
    private readonly ILogRepository logRepository;

    public LogService(ILogRepository logRepository)
    {
        this.logRepository = logRepository;
    }
    public async Task CreateNewLogAsync(Log newLog)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(newLog.Url);

        await this.logRepository.CreatableAsync(newLog);
    }

}
