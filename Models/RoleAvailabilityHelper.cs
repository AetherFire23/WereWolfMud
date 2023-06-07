using WereWolfMud.Utils;
using WereWolfUltraCool.Entities;
using WereWolfUltraCool.Enums;

namespace WereWolfMud.Models
{
    public class RoleAvailabilityHelper
    {
        private List<RoleAvailability> RoleAvailabilities { get; set; }
        private List<RoleAvailability> AvailableRoles => RoleAvailabilities.Where(x => x.IsAvailable).ToList();


        public RoleAvailabilityHelper(int playerCount) // 8 role hardcode for now 
        {
            InitializeAvailableRolesInGame(playerCount);
        }

        public void InitializeAvailableRolesInGame(int playerCount)
        {
            InitTownRoles();
            InitEvilRoles();
            InitNeutralRoles();
        }

        private void InitTownRoles()
        {
            RoleAvailabilities.Add(new RoleAvailability(RoleUtils.GetRandomTownInvestigative()));
            RoleAvailabilities.Add(new RoleAvailability(RoleUtils.GetRandomTownProtective()));
            RoleAvailabilities.Add(new RoleAvailability(RoleUtils.GetRandomTown()));
            RoleAvailabilities.Add(new RoleAvailability(RoleUtils.GetRandomTown()));
            RoleAvailabilities.Add(new RoleAvailability(RoleUtils.GetRandomTown()));
        }

        private void InitEvilRoles()
        {
            RoleAvailabilities.Add(new RoleAvailability(RoleUtils.GetRandomEvil()));
            RoleAvailabilities.Add(new RoleAvailability(RoleUtils.GetRandomEvil()));
        }

        private void InitNeutralRoles()
        {
            RoleAvailabilities.Add(new RoleAvailability(RoleUtils.GetRandomNeutral()));
        }

        public void AssignRandomRoleToPlayer(Player player)
        {
            if(player.RoleType != RoleType.NotAssigned)
            {
                throw new Exception("All player role already inited");
            }

            var randomRole = RandomHelper.GetRandomFrom(this.AvailableRoles);
            randomRole.IsAvailable = false;
            player.RoleType = randomRole.RoleType;
        }
    }
}
