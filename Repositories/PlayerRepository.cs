using Microsoft.EntityFrameworkCore;
using WereWolfMud.Entities;
using WereWolfMud.Utils;
using WereWolfUltraCool;
using WereWolfUltraCool.Entities;
using WereWolfUltraCool.Enums;
using WereWolfUltraCool.Interfaces;

namespace WereWolfMud.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly WereContext _context;
        public PlayerRepository(WereContext context)
        {
            _context = context;
        }

        public async Task<Player> GetPlayerAsync(Guid playerId)
        {
            var player = await _context.Players.FirstAsync(x => x.Id == playerId);
            return player;
        }

        public async Task<List<Player>> GetPlayersInGameAsync(Guid gameId)
        {
            var players = await _context.Players.Where(x => x.GameId == gameId).ToListAsync();
            return players;
        }

        public async Task<List<Player>> GetPlayersWithRoleAsync(Guid gameId, RoleType roleType)
        {
            var players = await _context.Players.Where(x => x.GameId == gameId && x.RoleType == roleType).ToListAsync();
            return players;
        }


        public async Task<List<Player>> GetAlivePlayersWithTownRolesAsync(Guid gameId)
        {
            var playersInGame = await GetPlayersInGameAsync(gameId);
            var playersWithTownROles = playersInGame.Where(x => x.IsAlive && RoleUtils.IsTownRole(x.RoleType)).ToList();
            return playersWithTownROles;
        }
        public async Task<List<Player>> GetAlivePlayersWithEvilRolesAsync(Guid gameId)
        {
            var playersInGame = await GetPlayersInGameAsync(gameId);
            var playersWithTownROles = playersInGame.Where(x => x.IsAlive && RoleUtils.IsEvilRole(x.RoleType)).ToList();
            return playersWithTownROles;
        }

        public async Task<List<TrialVote>> GetTrialVotesForPlayer(Guid playerId)
        {
            var votes = await _context.TrialVotes.Where(x => x.TargetPlayerId == playerId).ToListAsync();
            return votes;
        }
        public async Task<List<TrialVote>> GetTrialVotesFromPlayer(Guid playerId)
        {
            var votes = await _context.TrialVotes.Where(x => x.FromPlayerId == playerId).ToListAsync();
            return votes;
        }

        public async Task SetPlayerVote(Guid fromId, Guid targetId)
        {
            var votes = await GetTrialVotesFromPlayer(fromId);
            foreach (var vote in votes)
            {
                vote.TargetPlayerId = targetId;
            }
            await _context.SaveChangesAsync();
        }

        public async Task AbstainFromVoting(Guid playerId)
        {
            var votes = await GetTrialVotesFromPlayer(playerId);
            foreach (var vote in votes)
            {
                vote.TargetPlayerId = Guid.Empty;
            }
        }

        public async Task<List<TrialVote>> GetAllTrialVotesInGame(Guid gameId)
        {
            var trialVotes = await _context.TrialVotes.Where(x => x.GameId == gameId).ToListAsync();
            return trialVotes;
        }

        public async Task<Game> GetGameAsync(Guid gameId)
        {
            var game = await _context.Games.FirstAsync(x => x.Id == gameId);
            return game;
        }
    }
}
