using Microsoft.EntityFrameworkCore;
using WereWolfMud.Entities;
using WereWolfUltraCool;

namespace WereWolfMud.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly WereContext _context;
        public LogRepository(WereContext context)
        {
            _context = context;
        }
        public async Task SavePublicLog(EventLog eventLog)
        {
            await _context.EventLogs.AddAsync(eventLog);
            await _context.SaveChangesAsync();
        }

        public async Task SavePrivateLog(EventLog eventLog, LogAccessPermission logAccessPermission)
        {
            await _context.EventLogs.AddAsync(eventLog);
            await _context.LogAccessPermissions.AddAsync(logAccessPermission);
            await _context.SaveChangesAsync();
        }
        public async Task SavePrivateLog(EventLog eventLog, List<LogAccessPermission> logAccessPermissions)
        {
            await _context.EventLogs.AddAsync(eventLog);
            await _context.LogAccessPermissions.AddRangeAsync(logAccessPermissions);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EventLog>> GetPrivateLogsForPlayer(Guid playerId)
        {
            List<Guid> logIds = await _context.LogAccessPermissions.Where(x => x.AccessibleBy == playerId)
                .Select(x => x.LogId).ToListAsync();

            List<EventLog> logs = await _context.EventLogs.Join(logIds,
                e => e.Id,
                p => p,
                (e, p) => e).ToListAsync();
            return logs;
        }

        public async Task<List<EventLog>> GetPublicLogsForGame(Guid gameId)
        {
            var logs = await _context.EventLogs.Where(x => !x.IsPrivateLog).ToListAsync();
            return logs;
        }
    }
}
