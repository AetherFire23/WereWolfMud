using WereWolfMud.Interfaces;
using WereWolfUltraCool.Enums;

namespace WereWolfUltraCool.Entities
{
    public class Player : IEntity
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }

        public string Name { get; set; } = string.Empty;
        public RoleType RoleType { get; set; } = RoleType.NotAssigned;

        public int Number { get; set; } = -1; // -1 = unassigned

        public bool IsAlive { get; set; } = true;
        public override string ToString()
        {
            return this.Name;
        }
    }
}
