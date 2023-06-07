using WereWolfMud.Entities;
using WereWolfUltraCool.Entities;
using WereWolfUltraCool.Enums;

namespace WereWolfUltraCool.Interfaces
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAlivePlayersWithEvilRolesAsync(Guid gameId);
        Task<List<Player>> GetAlivePlayersWithTownRolesAsync(Guid gameId);
        Task<List<TrialVote>> GetAllTrialVotesInGame(Guid gameId);
        Task<Game> GetGameAsync(Guid gameId);
        Task<Player> GetPlayerAsync(Guid playerId);
        Task<List<Player>> GetPlayersInGameAsync(Guid gameId);
        Task<List<Player>> GetPlayersWithRoleAsync(Guid gameId, RoleType roleType);
        Task<List<TrialVote>> GetTrialVotesForPlayer(Guid playerId);
        Task AbstainFromVoting(Guid playerId);
        Task SetPlayerVote(Guid fromId, Guid targetId);
    }
}