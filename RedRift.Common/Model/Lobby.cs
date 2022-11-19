using System;
using System.Collections.Generic;
using System.Linq;

namespace RedRift.Common.Model
{
    public class Lobby : ILobby
    {
        public Guid Id { get; set; }

        public IPlayer Host { get => Players?.FirstOrDefault(); }

        public List<IPlayer> Players { get; set; }

        public int MaxPlayers { get; set; }

        public LobbyStatus LobbyStatus { get; set; }
    }
}
