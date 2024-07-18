using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Domain.Entities;
using System.Domain.Specification;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Repository
{
    public static class SpecifictionEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifiction<T> spec)
        {
            var query = inputQuery;

            if (spec.Cretiria is not null)
            {
                query = query.Where(spec.Cretiria);
            }

            query = spec.Includes.Aggregate(query, (cuurentquery, includeexprssion) => query.Include(includeexprssion));

            return query;
        }
    }
}
