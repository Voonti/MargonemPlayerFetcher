using Common;
using MargoFetcher.Domain.Exceptions;
using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Application.Items.Queries
{
    public record GetItemQuery(string hid) : IRequest<GetItemResult>;
    public record GetItemResult(Item item);
    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, GetItemResult>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<GetItemResult> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var result = await _itemRepository.GetItemsByHid(request.hid);
            if (result == null)
                throw new ItemNotFoundException(ExceptionsMessages.ItemNotFound);
            return new GetItemResult(result);
        }
    }

}
