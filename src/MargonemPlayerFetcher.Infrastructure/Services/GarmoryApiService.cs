using Common;
using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MargoFetcher.Infrastructure.Services
{
    public class GarmoryApiService : IGarmoryApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GarmoryApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Dictionary<string, Item>> FetchPlayerItems(Player player)
        {
            var catalog = Convert.ToInt32(player.charId) % 128;
            var query = $"{player.server}/{catalog}/{player.charId}.json";

            var httpClient = _httpClientFactory.CreateClient(GlobalParameters.ITEM_CLIENT);

            var response = await httpClient.GetAsync(query);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Dictionary<string, Item>>(content);
        }

        public async Task FetchPlayers(string server)
        {
            var query = $"{server}.json";

            var httpClient = _httpClientFactory.CreateClient(GlobalParameters.PLAYER_CLIENT);

            var response = await httpClient.GetAsync(query);

            var content = await response.Content.ReadAsStringAsync();

            var players = JsonConvert.DeserializeObject<IEnumerable<Player>>(content);

            var tasks = players.Select(i => AssignServer(i, server));

            await Task.WhenAll(tasks);
        }

        private async Task AssignServer(Player player, string server)
        {
            player.server = server;
        }
    }
}
