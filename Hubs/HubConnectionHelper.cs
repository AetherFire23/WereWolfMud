using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using WereWolfUltraCool.Entities;

namespace WereWolfMud.Hubs
{
    public class HubConnectionHelper
    {
        public HubConnection? _hubConnection;
        public bool IsConnected =>
            _hubConnection?.State == HubConnectionState.Connected;

        private bool CanDispose => _hubConnection is not null && !IsConnected;

        public HubConnectionHelper()
        {
        }

        public async Task<HubConnectionHelper> ConfigureHub(Uri uri)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(uri)
                .Build();

            await _hubConnection.StartAsync();
            return this;
        }

        public async Task DisposeIfPossible()
        {
            if (CanDispose)
            {
                await _hubConnection.DisposeAsync();
            }
        }

        public async Task SendTaskIfPossible(string method, object? arg1)
        {
            if(_hubConnection is not null && IsConnected)
            {
                await _hubConnection.SendAsync(method, arg1);
            }
        }
    }
}
