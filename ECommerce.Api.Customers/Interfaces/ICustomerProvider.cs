using ECommerce.Api.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool isSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool isSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
