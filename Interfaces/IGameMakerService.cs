using WereWolfUltraCool.Entities;

namespace WereWolfUltraCool.Interfaces
{
    public interface IGameMakerService
    {
        Task LaunchNewGame(Guid lobbyId);
    }
}