using BattleshipGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipGame.Services {
    public interface IGameService {
        GameState GetGameState();
        bool FireShot(int x, int y);
        bool CheckIfAllShipsSunk();
    }

    public class GameService : IGameService {
        private readonly GameState _gameState;

        public GameService() {
            _gameState = new GameState();
        }

        public GameState GetGameState() {
            return _gameState;
        }

        public bool FireShot(int x, int y) {
            return _gameState.FireShot(x, y);
        }

        public bool CheckIfAllShipsSunk() {
            return _gameState.CheckIfAllShipsSunk();
        }
    }
}
