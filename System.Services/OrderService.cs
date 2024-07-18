using System;
using System.Collections.Generic;
using System.Domain;
using System.Domain.Entities;
using System.Domain.Services.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Services
{
    public class OrderService : IOrderServices
    {
        private readonly IGenericRepository<Order> _Orderrepository;
        private readonly IGenericRepository<Product> _Productrepository;

        public OrderService(IGenericRepository<Order> Orepository, IGenericRepository<Product> Prepository)
        {
            _Orderrepository = Orepository;
            _Productrepository = Prepository;
        }
        public async Task<Order> CreateOrderAsync(string paytMethod, int custmorid, List<OrderItem> orderItems)
        {
            var Items = new List<OrderItem>();
            var paymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), paytMethod);

            foreach (var item in orderItems)
            {
                var product = await _Productrepository.GetByIdAsync(item.ProductId);
                int itemQuantity;
                if (product.Stock < item.Quantity)
                {
                    return null;
                }
                else
                {
                    itemQuantity = item.Quantity;
                    product.Stock -= itemQuantity;
                    await _Productrepository.UpdateAsync(product);
                }
                Items.Add(
                    new OrderItem()
                    {
                        UnitPrice = product.Price - (product.Price * item.Discount / 100),
                        ProductId = item.ProductId,
                        Discount = product.Price * item.Discount / 100,
                        Quantity = item.Quantity > product.Stock ? product.Stock : item.Quantity,
                    }
                    );
            };

            var order = new Order()
            {
                CustomerId = custmorid,
                PaymentMethod = paymentMethod,
                OrderItems = Items,

            };
            order.TotalAmount = order.OrderItems.Sum(OI => OI.Quantity * OI.UnitPrice);

            await _Orderrepository.AddAsync(order);

            return await _Orderrepository.GetByIdAsync(order.Id);
        }
    }
}
