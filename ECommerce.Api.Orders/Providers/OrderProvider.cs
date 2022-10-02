using AutoMapper;
using ECommerce.Api.Orders.Data;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrdersDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrderProvider(OrdersDbContext context, IMapper mapper, ILogger<OrderProvider> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;

            SeedData();
        }

        private void SeedData()
        {
            if (!_context.Orders.Any())
            {
                _context.Orders.Add(new Data.Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Total = 10,
                    OrderItems = new List<Data.OrderItem>()
                    {
                        new Data.OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    }
                });
                _context.Orders.Add(new Data.Order(){
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Total = 19,
                    OrderItems = new List<Data.OrderItem>() 
                    {
                        new Data.OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new Data.OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    }
                });

                _context.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await _context.Orders
                                    .Where(o => o.CustomerId == customerId)
                                    .Include(o => o.OrderItems)
                                    .ToListAsync();

                if(orders != null && orders.Any())
                {
                    var result = _mapper.Map<IEnumerable<Data.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not Found");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, null, "Not Found");
            }
        }
    }
}
