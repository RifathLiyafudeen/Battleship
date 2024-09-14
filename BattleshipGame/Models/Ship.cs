using System.Collections.Generic;
using System.Linq;

namespace BattleshipGame.Models {
    public class Ship {
        public string Name { get; set; }
        public List<(int x, int y)> Positions { get; set; }
        public int Size => Positions.Count;

        // Add a reference to the Grid if needed
        private Grid _grid;

        public Ship(string name, List<(int x, int y)> positions, Grid grid) {
            Name = name;
            Positions = positions;
            _grid = grid;
        }

        public bool IsSunk => Positions.All(pos => _grid.Cells[pos.x, pos.y].IsHit);
    }


}
