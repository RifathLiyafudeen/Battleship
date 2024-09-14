using BattleshipGame.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipGame.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService) {
            _gameService = gameService;
        }

        [HttpGet("board")]
        public IActionResult GetBoard() {
            var gameState = _gameService.GetGameState();
            return Ok(gameState);
        }

        [HttpPost("fire")]
        public IActionResult FireShot([FromBody] FireShotRequest request) {
            if (request.X < 0 || request.X >= 10 || request.Y < 0 || request.Y >= 10) {
                return BadRequest("Coordinates out of range.");
            }

            var hit = _gameService.FireShot(request.X, request.Y);
            var allSunk = _gameService.CheckIfAllShipsSunk();

            return Ok(new { Hit = hit, AllSunk = allSunk });
        }
    }

    public class FireShotRequest {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
