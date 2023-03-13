using MargonemPlayerFetcher.Domain.DTO;
using MargonemPlayerFetcher.Domain.Entities;
using MargonemPlayerFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Application.Items.Commands
{
    public record InsertItemCommand(IEnumerable<ItemDTO> items) : IRequest<bool>;
    public class InsertItemsCommandHandler : IRequestHandler<InsertItemCommand, bool>
    {
        private readonly IItemRepository _itemRepository;
        public InsertItemsCommandHandler(
            IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<bool> Handle(InsertItemCommand request, CancellationToken cancellationToken)
        {
            var itemsToInsert = new List<Item>();
            foreach (var item in request.items)
            {
                itemsToInsert.Add(new Item()
                {
                    Id = item.Id,
                    userId = item.userId,
                    charId = item.charId,
                    hid = item.hid,
                    name = item.name,
                    icon = item.icon,
                    st = item.st,
                    stat = item.stat,
                    tpl = item.tpl,
                    rarity = item.rarity,
                    lastFetchDate = DateTime.Now,
                    fetchDate = DateTime.Now
                });
            }

            return await _itemRepository.InsertItems(itemsToInsert);
        }
    }
}
