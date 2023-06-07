namespace WereWolfUltraCool.Entities
{
    public class Game
    {
        public Guid Id { get; set; } 

        public bool IsDay { get; set; }
        public bool MustRevealDeadPlayerROle { get; set; }
        public bool IsSelectingTargets { get; set; }
        
    }
}
