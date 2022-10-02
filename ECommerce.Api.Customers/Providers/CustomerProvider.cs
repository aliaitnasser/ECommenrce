using AutoMapper;
using ECommerce.Api.Customers.Data;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CustomerProvider(CustomerDbContext context, IMapper mapper, ILogger<CustomerProvider> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            SeedData();
        }

        private void SeedData()
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new Data.Customer() {Id = 1, Name= "Ali Ait Nasser", Address ="Rabat" });
                _context.Customers.Add(new Data.Customer() {Id = 2, Name= "Mariam Khouiammi", Address ="Marrakech" });
                _context.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();

                if(customers != null && customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<Data.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "NotFound");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, null, ex.Message);
                throw;
            }
        }

        public async Task<(bool isSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

                if (customer != null)
                {
                    var result = _mapper.Map<Data.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "NotFound");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, null, ex.Message);
                throw;
            }
        }

    }
}
