using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Product_Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 10;
        private int _pageSize = 5;

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }


    }
}
