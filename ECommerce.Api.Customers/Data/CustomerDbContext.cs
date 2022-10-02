using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
