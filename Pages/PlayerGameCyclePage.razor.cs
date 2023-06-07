using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Specialized;
using WereWolfMud.Hubs;
using WereWolfUltraCool.Entities;
using WereWolfUltraCool.SignalR;

namespace WereWolfMud.Pages;

public partial class PlayerGameCyclePage
{
    private HubConnectionHelper _hubConnectionHelper;
    private List<Player> _players = new List<Player>();

    // component for target selection ?
    private bool _isDay = false;
    private bool _mustShowAbilityTargets = false;
    private bool _isShowingGuiltyOrInnocentPrompt = false;


    [Parameter]
    public Guid GameId { get; set; } = Guid.Empty;

    protected override async Task OnInitializedAsync()
    {
        // _players =
        // Must Si tout le monde a vote, le coupable cest 
        // on redemande cest qui le guilty
       
        await ConfigureHub();
    }

    public async ValueTask DisposeAsync()
    {
        await _hubConnectionHelper.DisposeIfPossible();
    }
    private async Task ConfigureHub()
    {
        //_hubConnectionHelper = new HubConnectionHelper();

        //_hubConnectionHelper._hubConnection = new HubConnectionBuilder()
        //         .WithUrl(_navigationManager.ToAbsoluteUri("/GameHub"))
        //    .Build();

        //_hubConnectionHelper._hubConnection.On<Player>("OnPlayerJoined", OnPlayerJoined);

        //await _hubConnectionHelper._hubConnection.StartAsync();
    }
}