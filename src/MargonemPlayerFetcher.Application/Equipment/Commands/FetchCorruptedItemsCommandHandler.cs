using MargoFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Application.Equipment.Commands
{
    public record FetchCorruptedItemsCommand() : IRequest<Unit>;
    public class FetchCorruptedItemsCommandHandler : IRequestHandler<FetchCorruptedItemsCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IPrinter _printer;

        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        private int totalPlayersToHandle = 0;
        public FetchCorruptedItemsCommandHandler(
            IItemRepository itemRepository,
            IPrinter printer)
        {
            _itemRepository = itemRepository;
            _printer = printer;

        }

        public async Task<Unit> Handle(FetchCorruptedItemsCommand request, CancellationToken cancellationToken)
        {
            var duplicatedItems = await _itemRepository.GetDuplicatedItemsCount();
            _printer.PrintDuplicatedItems(duplicatedItems);
            return Unit.Value;
        }
    }
}
