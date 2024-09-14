using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipGame.Models {
    public class GameState {
        public Grid Grid { get; private set; }
        public List<Ship> Ships { get; private set; }

        public GameState() {
            Grid = new Grid();
            Ships = new List<Ship>();
            InitializeShips();
        }

        private void InitializeShips() {
            var random = new Random();
            Ships.Add(PlaceShip("Battleship", 5, random));
            Ships.Add(PlaceShip("Destroyer1", 4, random));
            Ships.Add(PlaceShip("Destroyer2", 4, random));
        }

        private Ship PlaceShip(string name, int size, Random random) {
            List<(int x, int y)> positions = new List<(int x, int y)>(); ;
            bool placed = false;

            while (!placed) {
                positions = GenerateRandomShipPositions(size, random);
                if (positions.All(pos => pos.x >= 0 && pos.x < 10 && pos.y >= 0 && pos.y < 10 && !Grid.Cells[pos.x, pos.y].ContainsShip)) {
                    placed = true;
                }
            }

            return new Ship(name, positions, Grid);
        }

        private List<(int x, int y)> GenerateRandomShipPositions(int size, Random random) {
            var positions = new List<(int x, int y)>();
            bool vertical = random.Next(2) == 0;
            int startX = random.Next(10);
            int startY = random.Next(10);

            if (vertical) {
                if (startX + size <= 10) {
                    for (int i = 0; i < size; i++) {
                        positions.Add((startX + i, startY));
                    }
                } else {
                    return GenerateRandomShipPositions(size, random);
                }
            } else {
                if (startY + size <= 10) {
                    for (int i = 0; i < size; i++) {
                        positions.Add((startX, startY + i));
                    }
                } else {
                    return GenerateRandomShipPositions(size, random);
                }
            }

            return positions;
        }

        public bool FireShot(int x, int y) {
            if (x < 0 || x >= 10 || y < 0 || y >= 10) {
                throw new ArgumentOutOfRangeException("Coordinates are out of range.");
            }

            var cell = Grid.Cells[x, y];
            if (cell.IsHit) {
                return false; // Already hit
            }

            cell.IsHit = true;
            if (cell.ContainsShip) {
                return Ships.Any(ship => ship.Positions.Contains((x, y)));
            }

            return false; // Missed
        }

        public void DisplayBoard() {
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    var cell = Grid.Cells[i, j];
                    if (cell.IsHit) {
                        Console.Write(cell.ContainsShip ? "X " : "O ");
                    } else {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }

        public bool CheckIfAllShipsSunk() {
            return Ships.All(ship => ship.IsSunk);
        }
    }
}
