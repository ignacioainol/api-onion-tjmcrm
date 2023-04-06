using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Views;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<Response<OrderDTO>>
    {
        //public string? OrderLocation { get; set; }
        public string OrderId { get; set; }
        //public string OrderLocation { get; set; }

        public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderDTO>>
        {
            private readonly IVPFService _vPFService;
            private readonly IMapper _mapper;

            public GetOrderByIdHandler(IVPFService vPFService, IMapper mapper)
            {
                _mapper = mapper;
                _vPFService = vPFService;
            }


            public async Task<Response<OrderDTO>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                Response<Order> order = await _vPFService.GetOrderById(request);
                var dto = _mapper.Map<OrderDTO>(order.Data);
                return new Response<OrderDTO>(dto);
            }
        }
    }
}
