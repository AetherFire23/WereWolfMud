namespace WereWolfMud.Entities
{
    public class LogAccessPermission
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid LogId { get; set; }
        public Guid AccessibleBy { get; set; }

    }
}
