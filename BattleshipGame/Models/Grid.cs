namespace BattleshipGame.Models {
    public class Grid {
        public Cell[,] Cells { get; set; }

        public Grid() {
            Cells = new Cell[10, 10];
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    Cells[i, j] = new Cell();
                }
            }
        }
    }

    public class Cell {
        public bool IsHit { get; set; }
        public bool ContainsShip { get; set; }
    }


}
