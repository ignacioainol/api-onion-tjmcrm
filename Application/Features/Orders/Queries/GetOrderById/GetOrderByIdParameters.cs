using Application.Parameters;

namespace Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdParameters : RequestParameter
    {
        public int? OrderId { get; set; }
    }
}
