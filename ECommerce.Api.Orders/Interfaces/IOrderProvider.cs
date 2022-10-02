using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
