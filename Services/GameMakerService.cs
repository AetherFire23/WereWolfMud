using WereWolfMud.Entities;
using WereWolfMud.Models;
using WereWolfMud.Utils;
using WereWolfUltraCool.Entities;
using WereWolfUltraCool.Enums;
using WereWolfUltraCool.Interfaces;

namespace WereWolfUltraCool.Services
{
    public class GameMakerService : IGameMakerService
    {
        private readonly WereContext _context;
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayerService _playerService;
        public GameMakerService(WereContext context, IPlayerRepository playerRepository, IPlayerService playerService)
        {
            _context = context;
            _playerRepository = playerRepository;
            _playerService = playerService;
        }

        public async Task LaunchNewGame(Guid lobbyId)
        {
            var game = new Game()
            {
                Id = lobbyId,
                IsDay = false,
                MustRevealDeadPlayerROle = false,
                IsSelectingTargets = true,
            };

            await _context.Games.AddAsync(game);
            await RollPlayerRoles(game.Id);
            await _context.SaveChangesAsync();
        }

        public async Task RollPlayerRoles(Guid gameId) // mettons 8 joueurs
        {
            var roleAvailabilities = new RoleAvailabilityHelper(8);
            var playersInGame = await _playerRepository.GetPlayersInGameAsync(gameId);

            foreach (var player in playersInGame)
            {
                roleAvailabilities.AssignRandomRoleToPlayer(player);
            }

            await _context.SaveChangesAsync();
        }

        // mettons 8 joueurs
        public async Task InitializeTrialVotes(Guid gameId)
        {
            var playersInGame = await _playerRepository.GetPlayersInGameAsync(gameId);

            var trialVotes = playersInGame.Select(p => new TrialVote()
            {
                Id = Guid.NewGuid(),
                GameId = gameId,
                FromPlayerId = p.Id,
                TargetPlayerId = Guid.NewGuid(),
            }).ToList();

            await _context.AddRangeAsync(trialVotes);
            await _context.SaveChangesAsync();
        }

        public async Task ResetTrialVotes(Guid gameId)
        {
            var trialVotes = await _playerRepository.GetAllTrialVotesInGame(gameId);
            foreach (var vote in trialVotes)
            {
                vote.TargetPlayerId = Guid.NewGuid();
            }

            await _context.SaveChangesAsync();
        }

        //TrialVotingRepository
        public async Task VoteForPlayer(Guid gameId, Guid votingPlayerId, Guid targetPlayer)
        {
            await _playerRepository.SetPlayerVote(votingPlayerId, targetPlayer);

            bool mustLynch = await CheckIfPlayerMustBeLynched(gameId, targetPlayer);

            if (mustLynch)
            {
                await _playerService.LynchPlayer(gameId, targetPlayer);
            }
        }

        public async Task AbstainFromVoting(Guid playerId)
        {
            await _playerRepository.AbstainFromVoting(playerId);
            // trigger refresh ?

        }

        private async Task<bool> CheckIfPlayerMustBeLynched(Guid gameId, Guid playerId)
        {
            var player = await _playerRepository.GetPlayerAsync(playerId);

            float playersInGame = (float)(await _playerRepository.GetPlayersInGameAsync(gameId)).Count();

            int votesRequiredForLynch = CalculateRequiredVotesToLynch(playersInGame);
            int voteCount = (await _playerRepository.GetTrialVotesForPlayer(playerId)).Count();

            bool isLynched = votesRequiredForLynch == voteCount;
            return isLynched;
        }

        private int CalculateRequiredVotesToLynch(float playersInGame)
        {
            bool isOdd = (float)playersInGame % 2f == 0f;
            int required = isOdd
                ? (int)Math.Ceiling(playersInGame / 2) // when odd, round up
                : (int)playersInGame + 1;// if even, means for examaple 12 players / 2 = 6vs6 which means 7 required, 
            return required;
        }

        public async Task SkipTrialVote(Guid gameId)
        {

        }
    }
}
