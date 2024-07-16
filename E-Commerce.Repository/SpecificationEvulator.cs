using E_Commerce.Core.Models;
using E_Commerce.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository
{
	public static class SpecificationEvulator<TEntity> where TEntity : BaseModel
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> querySequence,ISpecification<TEntity> spec)
		{
			var query = querySequence;
			if (spec.Criteria is not null)
				query = query.Where(spec.Criteria);
			foreach (var include in spec.Includes)
			{
				query=query.Include(include); 
			}
			return query;
		}
	}
}
