using RedRift.Common.Model;

using System;
using System.Threading.Tasks;

namespace RedRift.BusinessLogic
{
    public interface ILobbyBusinessLogic
    {
        Guid CreateLobby(int maxPlayers, int hostPlayerId);
        Task JoinToLobby(Guid lobbyId, int playerId);
        Task StartGame(ILobby lobby);
    }
}