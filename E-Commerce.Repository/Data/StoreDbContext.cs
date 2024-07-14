using E_Commerce.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
	public class StoreDbContext:DbContext
	{
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options) 
        {
            
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


		}
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
	}
}
