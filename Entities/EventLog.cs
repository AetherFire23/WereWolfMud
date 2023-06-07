namespace WereWolfMud.Entities
{
    public class EventLog
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public string Message { get; set; } = string.Empty;

        public bool IsPrivateLog { get; set; }
    }
}
