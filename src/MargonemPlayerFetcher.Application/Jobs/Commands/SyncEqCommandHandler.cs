using Hangfire;
using MargoFetcher.Application.Jobs.Jobs;
using MargoFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Application.Jobs.Commands
{
    public record SyncEqCommand() : IRequest<Unit>;
    public class SyncEqCommandHandler : IRequestHandler<SyncEqCommand>
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IPlayerRepository _playerRepository;
        private readonly SyncEq _syncEq;

        public SyncEqCommandHandler(
            IBackgroundJobClient backgroundJobClient,
            IPlayerRepository playerRepository,
            SyncEq syncEq)
        {
            _backgroundJobClient = backgroundJobClient;
            _playerRepository = playerRepository;
            _syncEq = syncEq;

        }

        public async Task<Unit> Handle(SyncEqCommand request, CancellationToken cancellationToken)
        {
            var servers = await _playerRepository.GetServers();
            foreach (var server in servers)
            {
                _backgroundJobClient.Enqueue(() => _syncEq.Execute(server.ServerName));
            }
            return Unit.Value;
        }
    }
}
