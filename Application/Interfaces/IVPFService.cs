using Application.Features.Items.Queries.GetItemsByOrderIdQuery;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Wrappers;
using Domain.Views;

namespace Application.Interfaces
{
    public interface IVPFService
    {
        Task<PageResponse<List<Order>>> GetOrders(GetAllOrdersQuery request);
        Task<Response<Order>> GetOrderById(GetOrderByIdQuery request);
        Task<Response<List<Item>>> GetItemsByOrderId(GetItemsByOrderIdQuery request);
    }
}
