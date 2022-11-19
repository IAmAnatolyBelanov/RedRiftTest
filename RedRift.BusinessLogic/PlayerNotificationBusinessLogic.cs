using RedRift.Common.Model;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedRift.BusinessLogic
{
    public class PlayerNotificationBusinessLogic : IPlayerNotificationBusinessLogic
    {
        public async Task NotifyPlayersAboutStartGame(ILobby lobby)
        {
            // Not implemented
        }

        public async Task NotifyAboutResults(IReadOnlyCollection<IPlayer> players, IGameResult gameResult)
        {
            // Not implemented
        }
    }
}
