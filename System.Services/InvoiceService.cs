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
    public class InvoiceService : IInvoice
    {
        private readonly IGenericRepository<Invoice> _repository;

        public InvoiceService(IGenericRepository<Invoice> repository)
        {
            _repository = repository;
        }
        public async Task<Invoice> CreateInvoice(int orderId, decimal TotalAmount)
        {
            var invoice = new Invoice()
            {
                OrderId = orderId,
                TotalAmount = TotalAmount
            };
            await _repository.AddAsync(invoice);

            return invoice;
        }
    }
}
