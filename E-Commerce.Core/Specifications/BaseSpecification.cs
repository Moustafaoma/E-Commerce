using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications
{
	public class BaseSpecification<T> : ISpecification<T> where T : BaseModel
	{
        public Expression<Func<T, bool>>? Criteria { get; set; } = null;
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public BaseSpecification()
        {
        }
        public BaseSpecification(Expression<Func<T, bool>> criteariaExpression)
        {
            Criteria= criteariaExpression;
        }
    }
}
