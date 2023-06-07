using Microsoft.AspNetCore.SignalR;
using WereWolfUltraCool.Entities;

namespace WereWolfUltraCool.SignalR
{
    public class GameHub : Hub
    {
        public const string Path = $"/{nameof(GameHub)}";

        public async Task OnPlayerJoined(Player player)
        {
            await Clients.All.SendAsync("OnPlayerJoined", player);
        }

        public async Task GameFulled()
        {
            await Clients.All.SendAsync(nameof(GameFulled));
        }
    }
}
