using WereWolfMud.Interfaces;

namespace WereWolfUltraCool.Entities
{
    public class Leader : IEntity
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
    }
}
