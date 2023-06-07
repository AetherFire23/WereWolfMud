using WereWolfMud.LocalInfo;

namespace WereWolfUltraCool.Interfaces
{
    public interface IPlayerService
    {
        Task<LocalStorageInfo> CreateNewLeaderAndLocalStorage();
        Task<(LocalStorageInfo localStorage, bool isLobbyFull)> AddPlayerToLobbyAndStartGameIfFull(Guid gameId, string name);
        Task LynchPlayer(Guid gameId, Guid playerId);
    }
}
