using System;
using System.Collections.Generic;
using System.Domain.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Specification
{
    public interface ISpecifiction<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> Cretiria { get; set; }
        List<Expression<Func<T, object>>> Includes { get; set; }


    }
}
