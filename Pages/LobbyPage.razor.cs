using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Specialized;
using WereWolfMud.Hubs;
using WereWolfUltraCool.Entities;
using WereWolfUltraCool.SignalR;

namespace WereWolfMud.Pages;

public partial class LobbyPage
{
    private HubConnectionHelper _hubConnectionHelper;

    private List<Player> _players = new List<Player>();

    [Parameter]
    public Guid GameId { get; set; } = Guid.Empty;

    protected override async Task OnInitializedAsync()
    {
        await FillPlayerList();
        await ConfigureHub();
    }

    private async Task FillPlayerList()
    {
        _players = await _playerRepository.GetPlayersInGameAsync(GameId);
    }

    private async Task OnPlayerJoined(Player player)
    {
        _players.Add(player);
        await InvokeAsync(StateHasChanged);
    }

    public async ValueTask DisposeAsync()
    {
        await _hubConnectionHelper.DisposeIfPossible();
    }
    private async Task ConfigureHub()
    {
        _hubConnectionHelper = new HubConnectionHelper();

        _hubConnectionHelper._hubConnection = new HubConnectionBuilder()
                 .WithUrl(_navigationManager.ToAbsoluteUri("/GameHub"))
            .Build();

        _hubConnectionHelper._hubConnection.On<Player>("OnPlayerJoined", OnPlayerJoined);

        await _hubConnectionHelper._hubConnection.StartAsync();
    }
}