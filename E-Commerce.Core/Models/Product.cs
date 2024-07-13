using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models
{
	public class Product:BaseModel
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        //Implemented in Fluent APi
        //[ForeignKey(nameof(Product.Brand))]
        public int BrandId { get; set; }
        public ProductBrand Brand { get; set; }
		//[ForeignKey(nameof(Product.Category))]
		public int CategoryId { get; set; }

        public ProductCategory Category { get; set; }
    }
}
