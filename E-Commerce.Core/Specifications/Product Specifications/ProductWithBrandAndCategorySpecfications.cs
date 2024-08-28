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
        public ProductWithBrandAndCategorySpecfications(string? sort,int?brandId,int?categoryId ):base(
			
			p=>
			(!brandId.HasValue||p.BrandId==brandId)&&
			(!categoryId.HasValue||p.CategoryId==categoryId)
			
			)
        {
            AddIncludes();
            if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{
					case "priceAsc":
						AddOrderBy(p => p.Price);
						break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
					default:
                        AddOrderBy(p => p.Name);
                        break;
                }
			}
			else
				AddOrderBy(p=>p.Name);
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
