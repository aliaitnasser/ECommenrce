using AutoMapper;
using ECommerce.Api.Products.Data;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Api.Products.Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                            .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                            .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productsProfile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(productsProfile));
            var mapper = new Mapper(config);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductsAsync();

            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnsProductWithValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                            .UseInMemoryDatabase(nameof(GetProductReturnsProductWithValidId))
                            .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productsProfile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(productsProfile));
            var mapper = new Mapper(config);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(1);

            Assert.True(product.Product.Id == 1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnsProductWithInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                            .UseInMemoryDatabase(nameof(GetProductReturnsProductWithInValidId))
                            .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productsProfile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(productsProfile));
            var mapper = new Mapper(config);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(-1);

            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for(int i = 1; i < 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.15)
                });
            }
            dbContext.SaveChanges();
        }
    }
}
