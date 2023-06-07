namespace WereWolfMud.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
    }
}
