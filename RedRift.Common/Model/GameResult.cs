using System;

namespace RedRift.Common.Model
{
    public class GameResult : IGameResult
    {
        // Just for SqLite
        public int Id { get; set; }

        public Guid LobbyId { get; set; }

        // It's better to use relations, but i tired with sqlite
        public int WinnerId { get; set; }

        public string PlayersJson { get; set; }
    }
}
