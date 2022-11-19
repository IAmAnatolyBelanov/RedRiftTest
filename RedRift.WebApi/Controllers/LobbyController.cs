using Microsoft.AspNetCore.Mvc;

using RedRift.BusinessLogic;
using RedRift.Common.Exceptions;

namespace RedRift.WebApi2.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LobbyController : ControllerBase
    {
        private readonly ILobbyBusinessLogic _lobbyBusinessLogic;

        public LobbyController(ILobbyBusinessLogic lobbyBusinessLogic)
        {
            _lobbyBusinessLogic = lobbyBusinessLogic;
        }

        // TODO - get hostPlayerId from authorization
        [HttpPost]
        public IActionResult CreateLobby(int maxPlayers, int hostPlayerId)
        {
            if (maxPlayers < 2)
            {
                throw new ValidationException("Players count must be >= 2");
            }

            var id = _lobbyBusinessLogic.CreateLobby(maxPlayers, hostPlayerId);

            return Ok(id);
        }

        // TODO - get playerId from authorization
        [HttpPost]
        public IActionResult JoinToLobby(Guid id, int playerId)
        {
            _lobbyBusinessLogic.JoinToLobby(id, playerId);

            return Ok();
        }
    }
}
