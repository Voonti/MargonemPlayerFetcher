using Common;
using MargoFetcher.Domain.Exceptions;
using MargonemPlayerFetcher.Domain.DTO;
using MargonemPlayerFetcher.Domain.Entities;
using MargonemPlayerFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Application.Players.Commands
{
    public record InsertPlayerCommand(PlayerDTO player) : IRequest<bool>;
    public class InsertPlayerCommandHandler : IRequestHandler<InsertPlayerCommand, bool>
    {
        private readonly IPlayerRepository _playerRepository;
        public InsertPlayerCommandHandler(
            IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        public async Task<bool> Handle(InsertPlayerCommand request, CancellationToken cancellationToken)
        {
            if (CheckIfValid(request.player))
            {
                throw new InvalidPlayerLevelException(ExceptionsMessages.InvalidPlayerLevel);
            }

            var player = new Player()
            {
                charId = request.player.charId,
                userId = request.player.userId,
                nick = request.player.nick,
                server = request.player.server,
                profession = request.player.profession,
                rank = request.player.rank,
                level = request.player.level
            };

            return await _playerRepository.InsertPlayer(player);
        }

        private bool CheckIfValid(PlayerDTO player)
        {
            if (player.level < GlobalParameters.PLAYERS_MIN_LVL)
                return false;
            return true;
        }
    }
}
