using MargoFetcher.Domain.Enums;
using MargoFetcher.Domain.DTO;
using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MargoFetcher.Application.Items.Commands
{
    public record InsertItemCommand(IEnumerable<ItemDTO> items) : IRequest<bool>;
    public class InsertItemsCommandHandler : IRequestHandler<InsertItemCommand, bool>
    {
        private readonly Regex eventRegex = new Regex("[0-9][0-9][0-9][0-9]");
        private readonly Regex licytRegex = new Regex("(Licytacja)");
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
                if (!CheckIfValidItem(item))
                {
                    continue;
                }

                itemsToInsert.Add(new Item()
                {
                    userId = item.userId,
                    charId = item.charId,
                    hid = item.hid,
                    name = item.name,
                    icon = item.icon,
                    st = item.st,
                    stat = item.stat,
                    tpl = item.tpl,
                    rarity = (char)getItemRarity(item),
                    lastFetchDate = DateTime.Now,
                    fetchDate = DateTime.Now
                });
            }

            return await _itemRepository.InsertItems(itemsToInsert);
        }

        private bool CheckIfValidItem(ItemDTO item)
        {
            var rarity = getItemRarity(item);
            if (rarity == RarityEnum.Artifact
                || rarity == RarityEnum.Legend
                || (isEventOrAuctionItem(item) && rarity != RarityEnum.Normal))
            {
                return true;
            }
            return false;
        }

        private RarityEnum getItemRarity(ItemDTO item)
        {
            var index = item.stat.IndexOf("rarity=");
            return (RarityEnum)item.stat[index + 7];
        }

        private bool isEventOrAuctionItem(ItemDTO item)
        {
            var startIndex = item.stat.IndexOf("opis=");
            if (startIndex == -1)
                return false;

            int endIndex = item.stat.IndexOf(";", startIndex);
            var description = item.stat.Substring(startIndex + 5, endIndex - startIndex);

            if (eventRegex.IsMatch(description)
                || licytRegex.IsMatch(description))
                return true;
            return false;
        }


    }
}
