using AutoMapper;
using E_Commerce.APIs.DTOs;
using E_Commerce.APIs.Errors;
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
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper)
		{
			_productRepo = productRepo;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllProductsAsync()
		{
			var spec=new ProductWithBrandAndCategorySpecfications();
			var products = await _productRepo.GetAllWithSpecAsync(spec);
			if (products is null)
				return NotFound(new ApiResponse(404));
			return Ok(_mapper.Map<IEnumerable<Product>,IEnumerable<ProductToReturnDto>>(products));
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProductAsync(int? id)
		{
			var spec = new ProductWithBrandAndCategorySpecfications(p=>p.Id==id);
			var product = await _productRepo.GetByIdWithSpecAsync(spec);
			if (product is null)
				return NotFound(new ApiResponse(404));
			return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
		}
    }
}
