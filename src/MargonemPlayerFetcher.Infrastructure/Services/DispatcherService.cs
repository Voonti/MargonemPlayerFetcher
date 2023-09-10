using MargoFetcher.Application.Equipment.Commands;
using MargoFetcher.Application.Jobs.Commands;
using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Infrastructure.Services
{
    public class DispatcherService : IDispatcherService
    {
        private readonly IMediator _mediator;
        private readonly IPlayerRepository _playerRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IPrinter _printer;
        private readonly IGarmoryApiService _garmoryApiService;
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(10);
        public DispatcherService(
            IMediator mediator,
            IPlayerRepository playerRepository,
            IItemRepository itemRepository,
            IPrinter printer,
            IGarmoryApiService garmoryApiService)
        {
            _mediator = mediator;
            _playerRepository = playerRepository;
            _itemRepository = itemRepository;
            _printer = printer;
            _garmoryApiService = garmoryApiService;
        }

        public async Task DispatchPlayerSync()
        {
            _printer.PrintStart(nameof(SyncPlayerCommandHandler));

            var servers = await _playerRepository.GetServers();

            var tasks = new List<Task>();
            foreach (var server in servers)
            {
                _printer.PrintServer(server.ServerName);

                var players = (await _garmoryApiService.FetchPlayers(server.ServerName));

                if (players == null)
                    continue;

                foreach (var player in players)
                {
                    var task = Task.Run(async () =>
                    {
                        await semaphore.WaitAsync();
                        try
                        {
                            await _mediator.Send(new SyncPlayerCommand(player));
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    });
                    tasks.Add(task);
                }

                _printer.PrintPlayerFetchStatus(players.Count());
            }
            await Task.WhenAll(tasks);
        }

        public async Task DispatchEquipmentSync()
        {
            _printer.PrintStart(nameof(SyncEquipmentCommandHandler));

            int totalPlayersToHandle = await _playerRepository.GetTotalPlayerCount();

            var servers = await _playerRepository.GetServers();

            var tasks = new List<Task>();

            foreach (var server in servers)
            {
                _printer.PrintServer(server.ServerName);

                var players = (await _playerRepository.GetAllPlayersByServer(server.ServerName));

                foreach (var player in players)
                {
                    var task = Task.Run(async () =>
                    {
                        await semaphore.WaitAsync();
                        try
                        {
                            await _mediator.Send(new SyncEquipmentCommand(player, Interlocked.Decrement(ref totalPlayersToHandle) - 1));
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    });
                    tasks.Add(task);
                }
            }
            await Task.WhenAll(tasks);
        }
    }
}
