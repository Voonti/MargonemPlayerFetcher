using Hangfire;
using MargoFetcher.Application.Jobs.Jobs;
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
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly SyncPlayer _syncPlayer;

        public SyncPlayerCommandHandler(
            IBackgroundJobClient backgroundJobClient,
            SyncPlayer syncPlayer)
        {
            _syncPlayer = syncPlayer;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Unit> Handle(SyncPlayerCommand request, CancellationToken cancellationToken)
        {
            _backgroundJobClient.Enqueue(() => _syncPlayer.Execute());
            return Unit.Value;
        }
    }
}
