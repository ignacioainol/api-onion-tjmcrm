using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Views;
using MediatR;

namespace Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<PageResponse<List<OrderDTO>>>
    {
        //public string? OrderNumber { get; set; }
        public string? OrderLocation { get; set; }
        public int PageSize { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public int Start { get; set; }
        public string? SortColumn { get; set; }
        public string? SortColumnDirection { get; set; }
        public string? SearchOrder { get; set; }
        public string? SalesPerson { get; set; }
        public string? SelectSequence { get; set; }
        public string? SelectStatus { get; set; }

        public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrdersQuery, PageResponse<List<OrderDTO>>>
        {
            //private readonly IRepositoryAsync<Order> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IVPFService _vPFService;

            public GetAllOrderQueryHandler(IMapper mapper, IVPFService vPFService)
            {
                //_repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _vPFService = vPFService;
            }
            //string connectionString = @"Provider=vfpoledb;Data Source=\\eavwwt01.tomjames.local\TEST\DBFS\Internet\ecom;Collating Sequence=machine;";

            public async Task<PageResponse<List<OrderDTO>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
            {
                PageResponse<List<Order>> orders = await _vPFService.GetOrders(request);

                List<OrderDTO>? orderDTO = _mapper.Map<List<OrderDTO>>(orders.Data.Skip(request.Start).Take(request.PageSize));
                return new PageResponse<List<OrderDTO>>(orderDTO, request.RecordsFiltered, request.RecordsTotal);
            }
        }
    }
}
