using E_Commerce.Core.Models;
using E_Commerce.Core.Repository.Contract;
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
			var products = await _productRepo.GetAllAsync();
			if (products is null)
				return NotFound();
			return Ok(products);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProductAsync(int? id)
		{
			if (id == null)
				return new BadRequestResult();
			var product = await _productRepo.GetByIdAsync(id.Value);
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
