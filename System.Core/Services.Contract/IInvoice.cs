using System;
using System.Collections.Generic;
using System.Domain.Entities;
using System.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Services.Contract
{
    public interface IInvoice
    {
        Task<Invoice> CreateInvoice(int orderId, decimal TotalAmount);
    }
}
