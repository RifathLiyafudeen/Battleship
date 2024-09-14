using BattleshipConsoleApp.Manager;
using BattleshipConsoleApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipConsoleApp {

    class Program {
        static async Task Main(string[] args) {
            var battleshipService = new BattleshipService();
            var gameManager = new GameManager(battleshipService);

            await gameManager.StartGameAsync();
        }
    }

}
