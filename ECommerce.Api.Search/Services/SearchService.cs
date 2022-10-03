using ECommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderServices _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public SearchService(IOrderServices orderService, IProductService productService, ICustomerService customerService)
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int CustomerId)
        {
            var orders = await _orderService.GetOrdersAsync(CustomerId);
            var customers = await _customerService.GetCustomerAsync(CustomerId);
            var products = await _productService.GetProductsAsync();

            if (orders.IsSuccess)
            {
                foreach(var order in orders.Orders)
                {
                    foreach (var item in order.OrderItems)
                    {
                        item.ProductName = products.IsSuccess ?
                                products.Products.FirstOrDefault(p => p.Id == item.ProductId).Name :
                                "Product Information are not available";
                    }
                }

                var result = new
                {
                    Customer = customers.IsSuccess ? customers.Customer : new { Name = "Customer information are not available"}, 
                    Orders = orders.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
