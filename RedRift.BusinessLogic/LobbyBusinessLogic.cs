using Microsoft.EntityFrameworkCore;

using RedRift.Common.Exceptions;
using RedRift.Common.Model;
using RedRift.DataAccess;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace RedRift.BusinessLogic
{
    public class LobbyBusinessLogic : ILobbyBusinessLogic
    {
        // TODO - use external cache, eg Redis
        private static readonly MemoryCache _memoryCache = new MemoryCache("Lobbies");

        private readonly IGameBusinessLogic _gameBusinessLogic;
        private readonly RedRiftContext _context;

        public LobbyBusinessLogic(IGameBusinessLogic gameBusinessLogic, RedRiftContext context)
        {
            _gameBusinessLogic = gameBusinessLogic;
            _context = context;
        }

        public Guid CreateLobby(int maxPlayers, int hostPlayerId)
        {
            var id = Guid.NewGuid();

            var cacheId = id.ToString();
            if (_memoryCache.Any(x => x.Key == cacheId))
            {
                throw new ValidationException($"Lobby with Id:{cacheId} already exist");
            }

            var player = _context.Players.FirstOrDefault(x => x.Id == hostPlayerId);
            if (player == null)
            {
                throw new DataException($"User with Id:{hostPlayerId} does not exist");
            }

            var lobby = new Lobby
            {
                Id = id,
                MaxPlayers = maxPlayers,
                Players = new List<IPlayer>(maxPlayers) { player },
                LobbyStatus = LobbyStatus.Created
            };

            _memoryCache.Set(cacheId, lobby, new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(10) });

            return id;
        }

        public async Task JoinToLobby(Guid lobbyId, int playerId)
        {
            var player = await _context.Players.FirstOrDefaultAsync(x => x.Id == playerId);
            var lobby = GetLobby(lobbyId);

            lock (lobby)
            {
                ValidateLobby(lobby);
                ValidateLobbyForPlayer(playerId, lobby);

                lobby.Players.Add(player);

                if (lobby.Players.Count() == lobby.MaxPlayers)
                {
                    StartGame(lobby).GetAwaiter().GetResult();
                }
            }
        }

        public async Task StartGame(ILobby lobby)
        {
            lobby.LobbyStatus = LobbyStatus.Started;

            await _gameBusinessLogic.Play(lobby, Environment.TickCount);
        }

        private ILobby GetLobby(Guid lobbyId)
        {
            var lobbyIdString = lobbyId.ToString();

            var lobby = _memoryCache.FirstOrDefault(x => x.Key == lobbyIdString).Value as ILobby;
            if (lobby == null)
            {
                throw new ValidationException($"Lobby with Id:{lobbyIdString} does not exist");
            }

            return lobby;
        }

        private void ValidateLobby(ILobby lobby)
        {
            if (lobby.LobbyStatus != LobbyStatus.Created)
            {
                throw new ValidationException("The game has started already");
            }

            if (lobby.Players.Count() >= lobby.MaxPlayers)
            {
                throw new ValidationException("Lobby is full");
            }
        }

        private void ValidateLobbyForPlayer(int playerId, ILobby lobby)
        {
            if (lobby.Players.Any(x => x.Id == playerId))
            {
                throw new ValidationException("Player already in lobby");
            }
        }
    }
}
