using Application.Parameters;

namespace Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrderParameters : RequestParameter
    {
        public string? OrderLocation { get; set; }
    }
}
