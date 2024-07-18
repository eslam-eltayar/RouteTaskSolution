using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagmentSystem.APIs.Errors;
using OrderManagmentSystem.APIs.Helpers;
using System.Domain;
using System.Domain.Entities;
using System.Domain.Services.Contract;
using System.Domain.Specification;

namespace OrderManagmentSystem.APIs.Controllers
{
    public class OrderController : ApiBaseController
    {
        private readonly IGenericRepository<Order> _repository;
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;
        private readonly IInvoice _invoice;

        public OrderController(IGenericRepository<Order> repository, IOrderServices orderServices, IMapper mapper,
            IInvoice invoice)
        {
            _repository = repository;
            _orderServices = orderServices;
            _mapper = mapper;
            _invoice = invoice;
        }

        // POST /api/orders - Create a new order

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderParams CreatedOrder)
        {
            var MappedItems = _mapper.Map<List<CreateOrderItemParams>, List<OrderItem>>(CreatedOrder.OrderItems);
            var order = await _orderServices.CreateOrderAsync(CreatedOrder.PaymentMethod, CreatedOrder.CustmorId, MappedItems);

            if (order is null) return BadRequest(new ApiResponse(400, "Not Enough Items At Stock"));

            var invoice = await _invoice.CreateInvoice(order.Id, order.TotalAmount);

            if (invoice is null) return BadRequest(new ApiResponse(400, "Error At Invoice Creation"));


            return Ok(order);
        }

        // GET /api/orders/{orderId} - Get details of a specific order

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var spec = new OrderWithOrderItemSpec(id);
            var order = await _repository.GetByIdAsyncWithSpec(spec);
            if (order is null) return NotFound(new ApiResponse(404));

            return Ok(order);
        }

        // GET /api/orders - Get all orders (admin only)

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrders()
        {
            var spec = new OrderWithOrderItemSpec();

            var orders = await _repository.GetAllAsyncWithSpec(spec);

            if (orders is null) return BadRequest(new ApiResponse(400));

            return Ok(orders);
        }

        // PUT /api/orders/{orderId}/status - Update order status (admin only)

        [HttpPut("{status},{orderid}")]
        public async Task<ActionResult<Order>> UpdateStatus(string status, int orderid)
        {
            var spec = new OrderWithOrderItemSpec(orderid);
            var order = await _repository.GetByIdAsyncWithSpec(spec);

            if (order is null) return NotFound(new ApiResponse(404));

            order.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), status);

            var result = await _repository.UpdateAsync(order);

            if (!(result > 0)) return BadRequest(new ApiResponse(400));
            return Ok(await _repository.GetByIdAsyncWithSpec(spec));

        }
    }
}
