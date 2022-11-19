using System;

namespace RedRift.Common.Model
{
    public interface IGameResult
    {
        int Id { get; set; }
        Guid LobbyId { get; set; }
        string PlayersJson { get; set; }
        int WinnerId { get; set; }
    }
}