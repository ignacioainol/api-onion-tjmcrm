using Application.Features.Items.Queries.GetItemsByOrderIdQuery;
using Microsoft.AspNetCore.Mvc;

namespace TJCMCRM.API.Controllers.v1
{
    public class ItemController : BaseApiController
    {
        [HttpGet()]
        //public async Task<IActionResult> Get(string orderId, string orderLocation)
        public async Task<IActionResult> Get(string orderId, string orderLocation)
        {
            return Ok(await Mediator.Send(new GetItemsByOrderIdQuery
            {
                OrderId = orderId,
                OrderLocation = orderLocation
            }));
        }
    }
}
