using System;
using System.Collections.Generic;
using System.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Specification
{
    public class LoginSpecification : BaseSpecification<User>
    {
        public LoginSpecification(string username)
            : base(u => u.Username.ToLower() == username.ToLower())
        {

        }
    }
}
