
using BattleshipConsoleApp.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipConsoleApp.Service {
    public class BattleshipService {
        private readonly HttpClient _client;

        public BattleshipService() {
            _client = new HttpClient {
                BaseAddress = new Uri("http://localhost:5000/api/"),
                Timeout = TimeSpan.FromSeconds(10)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> FireShotAsync(int x, int y) {
            var shot = new Shot { X = x, Y = y };
            var response = await _client.PostAsync("game/fire", new StringContent(JsonConvert.SerializeObject(shot), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode) {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseBody);
                return result.Hit;
            }
            return false;
        }

        public async Task<string> GetGameStateAsync() {
            var response = await _client.GetStringAsync("game/state");
            return response;
        }

        public async Task<bool> IsGameOverAsync() {
            var response = await _client.GetStringAsync("game/state");
            var state = JsonConvert.DeserializeObject<dynamic>(response);
            return state.AllSunk;
        }
    }
}


