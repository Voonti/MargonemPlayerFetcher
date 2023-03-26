using MargonemPlayerFetcher.Domain.Entities;
using MargonemPlayerFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Application.Players.Queries
{
    public record GetPlayersQuery() : IRequest<GetPlayersResult>;
    public record GetPlayersResult(IEnumerable<Player> players);
    public class GetPlayersQueryHandler : IRequestHandler<GetPlayersQuery, GetPlayersResult>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetPlayersQueryHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<GetPlayersResult> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            var result = await _playerRepository.GetAllPlayers();
            return new GetPlayersResult(result);
        }
    }
}
