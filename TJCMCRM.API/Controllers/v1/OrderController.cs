using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetOrderById;
using Microsoft.AspNetCore.Mvc;

namespace TJCMCRM.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class OrderController : BaseApiController
    {
        [HttpPost()]
        public async Task<IActionResult> Get([FromQuery] GetAllOrderParameters filter)
        {
            var requestInfo = Request.Form;
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchOrder = Request.Form["searchOrder"].FirstOrDefault();
            var salesP = Request.Form["salesP"].FirstOrDefault();
            var selectSequence = Request.Form["selectSequence"].FirstOrDefault();
            var selectStatus = Request.Form["selectStatus"].FirstOrDefault();

            return Ok(await Mediator.Send(new GetAllOrdersQuery
            {
                Start = Convert.ToInt32(start),
                PageSize = Convert.ToInt32(length),
                SortColumn = sortColumn,
                SortColumnDirection = sortColumnDirection,
                OrderLocation = filter.OrderLocation,
                SearchOrder = searchOrder,
                SalesPerson = salesP,
                SelectSequence = selectSequence,
                SelectStatus = selectStatus
            }));
        }

        // Get api/<controller>/5
        [HttpGet()]
        //public async Task<IActionResult> Get(string orderId, string orderLocation)
        public async Task<IActionResult> Get(string orderId)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery
            {
                OrderId = orderId
                //OrderLocation = orderLocation
            }));
        }

        [HttpGet()]
        public async Task<IActionResult> CheckStatus(string orderId)
        {
            return Ok("Hello World Check Status");
        }
    }
}
