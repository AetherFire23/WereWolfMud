using WereWolfUltraCool.Entities;

namespace WereWolfMud.RoleTasks
{
    public class GameTaskContext
    {
        public Player Player { get; set; } = new Player();
        public Guid PlayerId => Player.Id;
        public Guid GameId => Player.GameId;


        public TaskParameters TaskParameters { get; set; } = new TaskParameters();
    }
}
