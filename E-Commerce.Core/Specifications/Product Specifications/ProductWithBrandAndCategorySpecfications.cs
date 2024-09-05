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
        public ProductWithBrandAndCategorySpecfications(ProductSpecParams specParams ):base(
			
			p=>
			(!specParams.BrandId.HasValue||p.BrandId==specParams.BrandId)&&
			(!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId)&&
			(string.IsNullOrEmpty(specParams.Search))||p.Name.ToLower().Contains(specParams.Search.ToLower())
			
			)
        {
            AddIncludes();
            if (!string.IsNullOrEmpty(specParams.Sort))
			{
				switch (specParams.Sort)
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
		
			ApplyPagination(( specParams.PageIndex - 1) *specParams.PageSize, specParams.PageSize);
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
