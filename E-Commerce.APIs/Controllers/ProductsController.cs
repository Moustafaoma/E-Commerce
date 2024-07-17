using E_Commerce.Core.Models;
using E_Commerce.Core.Repository.Contract;
using E_Commerce.Core.Specifications;
using E_Commerce.Core.Specifications.Product_Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers
{

	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepo;
		

		public ProductsController(IGenericRepository<Product> productRepo)
		{
			_productRepo = productRepo;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsAsync()
		{
			var spec=new ProductWithBrandAndCategorySpecfications();
			var products = await _productRepo.GetAllWithSpecAsync(spec);
			if (products is null)
				return NotFound();
			return Ok(products);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProductAsync(int? id)
		{
			if (id == null)
				return new BadRequestResult();
			var spec = new ProductWithBrandAndCategorySpecfications(p=>p.Id==id);
			var product = await _productRepo.GetByIdWithSpecAsync(spec);
			if(product is null)
				return NotFound(new
				{
					message="Not Found",
					Status=404
				});
			return Ok(product);
		}
    }
}
