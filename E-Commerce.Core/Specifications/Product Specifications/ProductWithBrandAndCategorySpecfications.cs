using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Product_Specifications
{
	public class ProductWithBrandAndCategorySpecfications:BaseSpecification<Product>
	{
        public ProductWithBrandAndCategorySpecfications():base()
        {
			AddIncludes();
		}
        public ProductWithBrandAndCategorySpecfications(Expression<Func<Product,bool>> criteria):base(criteria)
        {
			AddIncludes();
		}
		private void AddIncludes()
		{
			Includes.Add(p => p.Brand);
			Includes.Add(p => p.Category);
		}
            
    }
}
