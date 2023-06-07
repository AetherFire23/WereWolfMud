using WereWolfMud.Interfaces;

namespace WereWolfMud.LocalInfo
{
    public class LocalStorageInfo
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsPlayer { get; set; }

        public LocalStorageInfo(IEntity entity, bool isPlayer, string name = "")
        {
            UserId = entity.Id;
            GameId = entity.Id;
            Name = name;
            IsPlayer = isPlayer;
        }
    }
}
