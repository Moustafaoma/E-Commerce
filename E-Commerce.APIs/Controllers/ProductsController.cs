using AutoMapper;
using E_Commerce.APIs.DTOs;
using E_Commerce.APIs.Errors;
using E_Commerce.APIs.Helpers;
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
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
																		   
        private readonly IMapper _mapper;								   

		public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper
			,IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductCategory> categoryRepo


            )
		{
			_productRepo = productRepo;
			_mapper = mapper;
			_brandRepo = brandRepo;
			_categoryRepo = categoryRepo;
		}
		[HttpGet]
		[ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

		public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetAllProductsAsync([FromQuery] ProductSpecParams specParams)
		{
			var spec=new ProductWithBrandAndCategorySpecfications(specParams);
			var products = await _productRepo.GetAllWithSpecAsync(spec);
			if (products is null)
				return NotFound(new ApiResponse(404));
			var data=_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);

			var countSpec=new ProductWithFilterationForCountSpec(specParams);

			var count= await _productRepo.GetCountAsync(countSpec);
			 
			return Ok (new Pagination<ProductToReturnDto>(specParams.PageSize, specParams.PageIndex
                , count, data));
        }
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToReturnDto>> GetProductAsync(int? id)
		{
			var spec = new ProductWithBrandAndCategorySpecfications(p=>p.Id==id);
			var product = await _productRepo.GetByIdWithSpecAsync(spec);
			if (product is null)
				return NotFound(new ApiResponse(404));
			return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
		}
		[HttpGet("brands")]
        [ProducesResponseType(typeof(ProductBrand), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrandsAsync()
		{
			var brands=await _brandRepo.GetAllAsync();
			return Ok(brands);
		}
		[HttpGet("Categories")]
        [ProducesResponseType(typeof(ProductCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategories()
		{
			var categories=await _categoryRepo.GetAllAsync();
			return Ok(categories);
		}

    }
}
