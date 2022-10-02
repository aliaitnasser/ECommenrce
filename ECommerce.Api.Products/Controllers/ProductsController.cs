using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductProvider _product;

        public ProductsController(IProductProvider product)
        {
            _product = product;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Product>>> GetAll()
        {
            var result = await _product.GetProductsAsync();
            if(result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Product>> GetPorduct(int id)
        {
            var result = await _product.GetProductAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound();
        }
    }
}
