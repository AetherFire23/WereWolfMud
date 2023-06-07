using Microsoft.EntityFrameworkCore;
using WereWolfMud.Attributes;
using WereWolfUltraCool;
using WereWolfUltraCool.Entities;
using WereWolfUltraCool.Enums;
using WereWolfUltraCool.Interfaces;

namespace WereWolfMud.RoleTasks
{
    // HAve multiple RoleAttribute if you want many tasks 

    [Role(RoleType.WereWolf)]
    public class WerewolfTask
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly WereContext _context;
        public WerewolfTask(IPlayerRepository playerRepository, WereContext context)
        {
            _context = context;
            _playerRepository = playerRepository;
        }

        public async Task<List<Player>> GetValidTargets()
        {
            var valid = await _context.Players.Where(x => x.RoleType != RoleType.WereWolf).ToListAsync();
            return valid;
        }

        public async Task ExecuteSkill(GameTaskContext context)
        {

        }

        // CreateLog
        
    }
}
