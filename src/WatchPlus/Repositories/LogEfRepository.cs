using WatchPlus.Data;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;

namespace WatchPlus.Repositories;

public class LogEfRepository : ILogRepository
{
    private readonly WatchPlusDbContext context;

    public LogEfRepository(WatchPlusDbContext context)
    {
        this.context = context;
    }
    public async Task CreatableAsync(Log log, IFormFile image)
    {
        log.Id = Guid.NewGuid();
        await context.Logs.AddAsync(log);
        await context.SaveChangesAsync();
    }

    

}
