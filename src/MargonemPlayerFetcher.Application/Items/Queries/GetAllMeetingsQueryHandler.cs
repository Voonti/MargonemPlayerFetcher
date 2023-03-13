using MargonemPlayerFetcher.Domain.Entities;
using MargonemPlayerFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargonemPlayerFetcher.Application.Items.Queries
{
    public record GetItemQuery(string hid) : IRequest<GetItemsResult>;
    public record GetItemsResult(Item item);
    public class GetAllMeetingsQueryHandler : IRequestHandler<GetItemQuery, GetItemsResult>
    {
        private readonly IItemRepository _itemRepository;

        public GetAllMeetingsQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<GetItemsResult> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var result = await _itemRepository.GetItemsByHid(request.hid);
            return new GetItemsResult(result);
        }
    }
}
