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
        private readonly SyncEq _syncEq;

        public SyncEqCommandHandler(
            IBackgroundJobClient backgroundJobClient,
            SyncEq syncEq)
        {
            _syncEq = syncEq;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Unit> Handle(SyncEqCommand request, CancellationToken cancellationToken)
        {
            _backgroundJobClient.Enqueue(() => _syncEq.Execute());
            return Unit.Value;
        }
    }
}
