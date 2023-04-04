using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Application.Jobs
{
    public class NotPresentItem : ItemStrategy
    {
        private readonly IItemRepository _itemRepository;

        public NotPresentItem(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public override async Task HandleItem(Item items)
        {
            await _itemRepository.InsertItem(items);
        }
    }
}
