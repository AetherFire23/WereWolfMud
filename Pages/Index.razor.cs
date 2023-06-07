using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Reflection.Metadata.Ecma335;
using WereWolfMud.Hubs;
using WereWolfMud.LocalInfo;
using WereWolfUltraCool.Entities;

namespace WereWolfMud.Pages
{
    public partial class Index
    {
        private HubConnectionHelper _hubConnectionHelper;

        public string NameValue { get; set; } = string.Empty;
        public string GameIdValue { get; set; } = string.Empty;
        private bool _playerSelected = false;
        private bool _isValidInput { get; set; } = true;
        private string ErrorMessage { get; set; } = string.Empty;

        private bool IsValidInput()
        {
            ErrorMessage = string.Empty;
            if (string.IsNullOrEmpty(NameValue) || string.IsNullOrEmpty(GameIdValue))
            {
                ErrorMessage += "Le nom du joueur ou de la partie ne peuvent etre vides";
                return false;
            }

            if (!Guid.TryParse(GameIdValue, out Guid result))
            {
                ErrorMessage += "Lid de la partie est invalide";
                return false;
            }

            return true;
        }

        private void ResetPagePropeties()
        {
            _playerSelected = false;
            _isValidInput = true;
        }

        private void NavigateToLobby(Guid gameId)
        {
            _navigationManager.NavigateTo($"/LobbyPage/{gameId}", true);
        }

        private async Task CreateNewLeader()
        {
            var newLocalStorage = await _playerService.CreateNewLeaderAndLocalStorage();
            await _storageService.SaveLocalInfo(newLocalStorage);

            NavigateToLobby(newLocalStorage.GameId);
        }

        private async Task TryCreateNewPlayer()
        {
            _isValidInput = IsValidInput();

            if (!_isValidInput) return;

            Guid gameId = new Guid(GameIdValue);
            (LocalStorageInfo localStorage, bool isLobbyFull) playerResult = await _playerService.AddPlayerToLobbyAndStartGameIfFull(gameId, NameValue);

            await _storageService.SaveLocalInfo(playerResult.localStorage);

            await _hubConnectionHelper.SendTaskIfPossible("OnPlayerJoined", new Player()
            {
                Id = playerResult.localStorage.UserId,
                Name = playerResult.localStorage.Name,
                GameId = gameId,
            });

            // dont navigate to lobby if it is full

            if (playerResult.isLobbyFull)
            {

            }


            NavigateToLobby(gameId);
        }

        protected override async Task OnInitializedAsync()
        {
            _hubConnectionHelper = new HubConnectionHelper();
            await _hubConnectionHelper
                .ConfigureHub(_navigationManager.ToAbsoluteUri("/GameHub"));
        }

        protected override async Task OnAfterRenderAsync(bool first)
        {
            if (first)
            {
                await _storageService.ClearAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            await _hubConnectionHelper.DisposeIfPossible();
        }
    }
}
