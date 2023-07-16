using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Application.Equipment
{
    public class PresentItem : ItemStrategy
    {
        private readonly IItemRepository _itemRepository;

        public PresentItem(
            IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public override async Task HandleItem(Item item)
        {
            await _itemRepository.UpdateFetchDate(item);
        }
    }
}
