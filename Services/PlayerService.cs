using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WereWolfMud.Interfaces;
using WereWolfMud.LocalInfo;
using WereWolfUltraCool.Entities;
using WereWolfUltraCool.Interfaces;
using WereWolfUltraCool.SignalR;

namespace WereWolfUltraCool.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly WereContext _context;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly IGameMakerService _gameMaker;
        private readonly IPlayerRepository _playerRepository;
        public PlayerService(IHubContext<GameHub> hubContext,
            WereContext context,
            IGameMakerService gameMaker,
            IPlayerRepository playerRepository)
        {
            _gameMaker = gameMaker;
            _hubContext = hubContext;
            _context = context;
            _playerRepository = playerRepository;
        }

        public async Task<(LocalStorageInfo localStorage, bool isLobbyFull)> AddPlayerToLobbyAndStartGameIfFull(Guid gameId, string name)
        {
            var localStorage = await CreateNewPlayerAndLocalStorage(gameId, name);

            var playerCount = await _context.Players.CountAsync(p => p.GameId == gameId);
            var isLobbyFull = playerCount > 5;
            if (isLobbyFull)
            {
                await _gameMaker.LaunchNewGame(gameId);
            }

            return (localStorage, isLobbyFull);
        }

        public async Task<LocalStorageInfo> CreateNewPlayerAndLocalStorage(Guid gameId, string name)
        {
            Player player = new Player()
            {
                Id = Guid.NewGuid(),
                GameId = gameId,
                Name = name,
            };
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            var localStorage = await CreateLocalStorage(player, true, player.Name);
            return localStorage;
        }

        public async Task<LocalStorageInfo> CreateNewLeaderAndLocalStorage()
        {
            var leader = new Leader()
            {
                Id = Guid.NewGuid(),
                GameId = Guid.NewGuid(),
            };

            var localStorage = await CreateLocalStorage(leader, false);
            await _context.Leaders.AddAsync(leader);
            return localStorage;
        }

        public async Task<LocalStorageInfo> CreateLocalStorage(IEntity entity, bool isPlayer, string name = "")
        {
            var localStorage = new LocalStorageInfo(entity, isPlayer, name);
            return localStorage;
        }

        public async Task KillPlayer(Guid playerId)
        {
            var player = await _playerRepository.GetPlayerAsync(playerId);
            player.IsAlive = false;
            await _context.SaveChangesAsync();
        }

        public async Task LynchPlayer(Guid gameId, Guid playerId)
        {
            var player = await _playerRepository.GetPlayerAsync(playerId);
            await KillPlayer(playerId);

            var game = await _playerRepository.GetGameAsync(gameId);
            game.MustRevealDeadPlayerROle = true;
            await _context.SaveChangesAsync();
        }

        public Task LynchPlayer(Guid playerId)
        {
            throw new NotImplementedException();
        }
    }
}
