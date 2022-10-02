using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider _order;

        public OrdersController(IOrderProvider order)
        {
            _order = order;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<IEnumerable<Models.Order>>> GetOrdersAsync(int customerId)
        {
            var result = await _order.GetOrdersAsync(customerId);
            if (result.isSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}
