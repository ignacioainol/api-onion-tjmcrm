using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Views;
using MediatR;

namespace Application.Features.Items.Queries.GetItemsByOrderIdQuery
{
    public class GetItemsByOrderIdQuery : IRequest<Response<List<ItemDTO>>>
    {
        public string OrderId { get; set; }
        public string OrderLocation { get; set; }

        public class GetItemsByOrderIdQueryHandler : IRequestHandler<GetItemsByOrderIdQuery, Response<List<ItemDTO>>>
        {
            private readonly IMapper _mapper;
            private readonly IVPFService _vPFService;

            public GetItemsByOrderIdQueryHandler(IMapper mapper, IVPFService vPFService)
            {
                _mapper = mapper;
                _vPFService = vPFService;
            }

            public async Task<Response<List<ItemDTO>>> Handle(GetItemsByOrderIdQuery request, CancellationToken cancellationToken)
            {
                Response<List<Item>> items = await _vPFService.GetItemsByOrderId(request);

                List<ItemDTO>? itemDto = _mapper.Map<List<ItemDTO>>(items.Data);
                return new Response<List<ItemDTO>>(itemDto);
            }
        }
    }
}
