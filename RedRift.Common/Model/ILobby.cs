using System;
using System.Collections.Generic;

namespace RedRift.Common.Model
{
    public interface ILobby
    {
        IPlayer Host { get; }
        Guid Id { get; set; }
        List<IPlayer> Players { get; set; }
        int MaxPlayers { get; set; }
        LobbyStatus LobbyStatus { get; set; }
    }
}