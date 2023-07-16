using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Application.Jobs.Commands
{
    public record SyncPlayerCommand() : IRequest<Unit>;
    public class SyncPlayerCommandHandler : IRequestHandler<SyncPlayerCommand>
    {
        private readonly IGarmoryApiService _garmoryApiService;
        private readonly IPlayerRepository _playerRepository;
        private readonly IPrinter _printer;

        public SyncPlayerCommandHandler(
            IGarmoryApiService garmoryApiService,
            IPlayerRepository playerRepository,
            IPrinter printer)
        {
            _garmoryApiService = garmoryApiService;
            _playerRepository = playerRepository;
            _printer = printer;
        }

        public async Task<Unit> Handle(SyncPlayerCommand request, CancellationToken cancellationToken)
        {
            _printer.PrintStart(nameof(SyncPlayerCommandHandler));

            var servers = await _playerRepository.GetServers();

            foreach (var server in servers)
            {
                _printer.PrintServer(server.ServerName);

                var players = (await _garmoryApiService.FetchPlayers(server.ServerName));

                if (players == null)
                    continue;

                //var saveTasks = new List<Task>();
                foreach (var player in players)
                {
                    await SavePlayer(player);
                    //var task = SavePlayer(player);s
                    //saveTasks.Add(task);
                }

                _printer.PrintPlayerFetchStatus(players.Count());

                //await Task.WhenAll(saveTasks);
            }

            return Unit.Value;
        }

        private async Task SavePlayer(Player player)
        {
            await _playerRepository.InsertPlayerIfNotExist(player);
            await _playerRepository.UpdatePlayersLevel(player);
        }
    }
}
