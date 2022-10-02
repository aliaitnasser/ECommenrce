using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerProvider _cutomer;

        public CustomersController(ICustomerProvider cutomer)
        {
            _cutomer = cutomer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Customer>>> GetAll()
        {
            var result = await _cutomer.GetCustomersAsync();
            if(result.isSuccess)
            {
                return Ok(result.Customers);
            }
            return NotFound();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Customer>> GetById(int id)
        {
            var result = await _cutomer.GetCustomerAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.Customer);
            }
            return NotFound();

        }
    }
}
