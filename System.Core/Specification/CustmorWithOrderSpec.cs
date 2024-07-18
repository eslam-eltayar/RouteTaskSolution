using System;
using System.Collections.Generic;
using System.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Specification
{
    public class CustmorWithOrderSpec : BaseSpecification<Customer>
    {
        public CustmorWithOrderSpec()
        {
            Includes.Add(C => C.Orders);
        }
        public CustmorWithOrderSpec(int id)
            : base(C => C.Id == id)
        {
            Includes.Add(C => C.Orders);
        }
    }
}
