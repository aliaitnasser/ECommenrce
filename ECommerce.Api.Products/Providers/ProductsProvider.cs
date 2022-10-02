using AutoMapper;
using ECommerce.Api.Products.Data;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductProvider
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedDate();
        }

        private void SeedDate()
        {
            if(!_dbContext.Products.Any())
            {
                _dbContext.Products.Add(new Data.Product() { Id = 1, Name = "T-shirt", Price = 20, Inventory = 100 });
                _dbContext.Products.Add(new Data.Product() { Id = 2, Name = "Jeans", Price = 60, Inventory = 300 });
                _dbContext.Products.Add(new Data.Product() { Id = 3, Name = "Hody", Price = 30, Inventory = 900 });
                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                if(products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Data.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
                throw;
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product != null)
                {
                    var result = _mapper.Map<Data.Product,Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
                throw;
            }
        }
    }
}
