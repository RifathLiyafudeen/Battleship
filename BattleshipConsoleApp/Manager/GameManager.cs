using BattleshipConsoleApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipConsoleApp.Manager {
    public class GameManager {
        private readonly BattleshipService _battleshipService;

        public GameManager(BattleshipService battleshipService) {
            _battleshipService = battleshipService;
        }

        public async Task StartGameAsync() {
            Console.WriteLine("Welcome to the Battleship Game!");

            while (true) {
                Console.WriteLine("Enter coordinates to fire (e.g., A5): ");
                var input = Console.ReadLine();

                if (TryParseCoordinates(input, out int x, out int y)) {
                    bool hit = await _battleshipService.FireShotAsync(x, y);
                    Console.WriteLine(hit ? "Hit!" : "Miss!");

                    var state = await _battleshipService.GetGameStateAsync();
                    Console.WriteLine("Current Game State:");
                    Console.WriteLine(state);

                    if (await _battleshipService.IsGameOverAsync()) {
                        Console.WriteLine("Congratulations! You've sunk all the ships!");
                        break;
                    }
                } else {
                    Console.WriteLine("Invalid input. Please enter coordinates in the format 'A1'.");
                }
            }
        }

        private bool TryParseCoordinates(string input, out int x, out int y) {
            x = -1;
            y = -1;

            if (input.Length != 2) return false;

            char column = input[0];
            char row = input[1];

            if (column < 'A' || column > 'J' || row < '1' || row > '9') return false;

            x = column - 'A';
            y = row - '1';

            return true;
        }
    }
}