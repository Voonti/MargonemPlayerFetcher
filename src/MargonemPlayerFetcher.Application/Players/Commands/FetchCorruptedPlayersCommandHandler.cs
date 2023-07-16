using MargoFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Application.Players.Commands
{
    public record FetchCorruptedPlayersCommand() : IRequest<Unit>;
    public class FetchCorruptedPlayersCommandHandler : IRequestHandler<FetchCorruptedPlayersCommand>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPrinter _printer;

        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        private int totalPlayersToHandle = 0;
        public FetchCorruptedPlayersCommandHandler(
            IPlayerRepository playerRepository,
            IPrinter printer)
        {
            _playerRepository = playerRepository;
            _printer = printer;

        }

        public async Task<Unit> Handle(FetchCorruptedPlayersCommand request, CancellationToken cancellationToken)
        {
            var duplicatedPlayers = await _playerRepository.GetDuplicatedPlayersCount();
            _printer.PrintDuplicatedPlayers(duplicatedPlayers);
            return Unit.Value;
        }
    }
}
