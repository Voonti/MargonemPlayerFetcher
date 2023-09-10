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
    public record SyncPlayerCommand(Player player) : IRequest<Unit>;
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
            await SavePlayer(request.player);
            return Unit.Value;
        }

        private async Task SavePlayer(Player player)
        {
            try
            {
                if (!await _playerRepository.CheckIfPlayerExists(player)) {
                    await _playerRepository.InsertPlayer(player);
                }
                else
                {
                    await _playerRepository.UpdatePlayerLevel(player);
                    await _playerRepository.UpdatePlayerName(player);
                    await _playerRepository.UpdatePlayerFetchDate(player);
                }
            }
            catch (Exception ex)
            {
                _printer.PrintException(ex.Message);
            }
        }
    }
}
