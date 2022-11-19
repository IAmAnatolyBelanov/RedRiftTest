using RedRift.Common.Model;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedRift.BusinessLogic
{
    public interface IPlayerNotificationBusinessLogic
    {
        Task NotifyAboutResults(IReadOnlyCollection<IPlayer> players, IGameResult gameResult);
        Task NotifyPlayersAboutStartGame(ILobby lobby);
    }
}