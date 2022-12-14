using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Data.Order, Models.Order>();
            CreateMap<Data.OrderItem, Models.OrderItem>();
        }
    }
}
