using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Linq;
using E_Commerce.Core.Models;

namespace E_Commerce.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext _dbContext)
        {
            if (_dbContext.ProductBrands.Count() == 0)
            {
				var brandsData = File.ReadAllText("../E-Commerce.Repository/Data/SeedData/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
				if (brands is not null&&brands.Count>0)
				{
					foreach (var brand in brands)
					{
						 _dbContext.Set<ProductBrand>().Add(brand);
					}
					await _dbContext.SaveChangesAsync();
				}

			}
			if (_dbContext.ProductCategories.Count() == 0)
			{
				var categoriesData = File.ReadAllText("../E-Commerce.Repository/Data/SeedData/categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
				if (categories is not null && categories.Count > 0)
				{
					foreach (var category in categories)
					{
						_dbContext.Set<ProductCategory>().Add(category);
					}
					await _dbContext.SaveChangesAsync();
				}

			}
			if (_dbContext.Products.Count() == 0)
			{
				var ProductsData = File.ReadAllText("../E-Commerce.Repository/Data/SeedData/products.json");
				var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
				if (Products is not null && Products.Count > 0)
				{
					foreach (var product in Products)
					{
						_dbContext.Set<Product>().Add(product);
					}
					await _dbContext.SaveChangesAsync();
				}

			}





		}
	}
}
