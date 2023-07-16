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
using System.Net.Http;
using Microsoft.Extensions.Http;

namespace MargoFetcher.Infrastructure.Services
{
    public class GarmoryApiService : IGarmoryApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPrinter _printer;

        public GarmoryApiService(
            IHttpClientFactory httpClientFactory,
            IPrinter printer)
        {
            _httpClientFactory = httpClientFactory;
            _printer = printer;
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

        public async Task<IEnumerable<Player>> FetchPlayers(string server)
        {
                var query = $"{server}.json";

                var httpClient = _httpClientFactory.CreateClient(GlobalParameters.PLAYER_CLIENT);

                var response = await httpClient.GetAsync(query);

                var content = await response.Content.ReadAsStringAsync();

                var players = DeserializePlayers(content);

                if (players == null)
                    return null;

                var tasks = players.Select(i => AssignServer(i, server));

                await Task.WhenAll(tasks);

                return players;
        }

        private IEnumerable<Player> DeserializePlayers(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<Player>>(content);
            }
            catch (Exception ex)
            {
                _printer.PrintException(ex.Message);
                return null;
            }
        }

        private async Task AssignServer(Player player, string server)
        {
            player.server = server;
        }
    }
}
