using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Product_Specifications
{
    public class ProductWithFilterationForCountSpec:BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpec(ProductSpecParams specParams) :base(

            p =>
            (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId) &&
            (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId)&&
            (string.IsNullOrEmpty(specParams.Search)) || p.Name.ToLower().Contains(specParams.Search.ToLower())

            )

        {
            
        }
    }
}
