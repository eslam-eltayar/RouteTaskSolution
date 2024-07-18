using System.Domain.Entities;
using System.Linq.Expressions;


namespace System.Domain.Specification
{
    public class BaseSpecification<T> : ISpecifiction<T> where T : BaseEntity
    {
        public BaseSpecification()
        {

        }

        public Expression<Func<T, bool>> Cretiria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public BaseSpecification(Expression<Func<T, bool>> expression)
        {
            Cretiria = expression;
        }
    }
}
