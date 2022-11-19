using RedRift.Common.Model;
using RedRift.DataAccess;

using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedRift.BusinessLogic
{
    public class GameBusinessLogic : IGameBusinessLogic
    {
        private readonly RedRiftContext _context;
        private readonly IPlayerNotificationBusinessLogic _playerNotificationBusinessLogic;

        public GameBusinessLogic(RedRiftContext context, IPlayerNotificationBusinessLogic playerNotificationBusinessLogic)
        {
            _context = context;
            _playerNotificationBusinessLogic = playerNotificationBusinessLogic;
        }

        public async Task Play(ILobby lobby, int randomSeed)
        {
            _playerNotificationBusinessLogic.NotifyPlayersAboutStartGame(lobby);
            InitializeGame(lobby);

            var random = new Random(randomSeed);

            while (lobby.Players.All(x => x.Health > 0))
            {
                foreach (var player in lobby.Players)
                {
                    var damage = random.Next(0, 3);
                    player.Health -= damage;
                }

                if (lobby.Players.All(x => x.Health <= 0))
                {
                    foreach (var player in lobby.Players)
                    {
                        player.Health = 1;
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            var result = new GameResult()
            {
                LobbyId = lobby.Id,
                PlayersJson = JsonSerializer.Serialize(lobby.Players.Select(x => x.Id)),
                WinnerId = lobby.Players.First(x => x.Health > 0).Id
            };

            await _context.GameResults.AddAsync(result);
            await _context.SaveChangesAsync();

            _playerNotificationBusinessLogic.NotifyAboutResults(lobby.Players, result);

            lobby.LobbyStatus = LobbyStatus.Ended;
        }

        private void InitializeGame(ILobby lobby)
        {
            foreach (var player in lobby.Players)
            {
                player.Health = player.BaseHealth;
            }
        }
    }
}
