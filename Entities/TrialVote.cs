using System.ComponentModel.DataAnnotations.Schema;

namespace WereWolfMud.Entities
{
    // should be unique for each player since you cant vote more than once.
    // The Crow, however, has 2 votes so yeah just account for that whatever
    public class TrialVote
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid FromPlayerId { get; set; }
        public Guid TargetPlayerId { get; set; }
        public bool IsHidden { get; set; }

        [NotMapped]
        public bool IsAbstaining => TargetPlayerId == Guid.Empty;
    }
}
