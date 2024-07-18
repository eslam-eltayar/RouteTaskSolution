using System;
using System.Collections.Generic;
using System.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Services.Contract
{
    public interface IOrderServices
    {
        Task<Order> CreateOrderAsync(string paymentMethod, int custmorid, List<OrderItem> orderItems);
    }
}
