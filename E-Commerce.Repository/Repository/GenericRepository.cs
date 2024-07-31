using E_Commerce.Core.Models;
using E_Commerce.Core.Repository.Contract;
using E_Commerce.Core.Specifications;
using E_Commerce.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Repository
{
	public class GenericRepository<T>:IGenericRepository<T> where T : BaseModel
	{
		private protected readonly StoreDbContext _context;
        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
		public async Task<IReadOnlyList<T>> GetAllAsync() =>
			await _context.Set<T>().AsNoTracking().ToListAsync();	
		
		public async Task<T?> GetByIdAsync(int id)
		{
			//if(typeof(T) == typeof(Product))
			//{
			//	return  await _context.Set<Product>().Include(p=>p.Brand).Include(p=>p.Category).FirstOrDefaultAsync(p=>p.Id==id) as T;
			//}
		 	return await _context.Set<T>().FindAsync(id);
		}
		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> specification)=>
		await ApplySpecifications(specification).AsNoTracking().ToListAsync();
		
		public async Task<T?> GetByIdWithSpecAsync(ISpecification<T> specification)
		{
			//if(typeof(T) == typeof(Product))
			//{
			//	return  await _context.Set<Product>().Include(p=>p.Brand).Include(p=>p.Category).FirstOrDefaultAsync(p=>p.Id==id) as T;
			//}
			return await ApplySpecifications(specification).FirstOrDefaultAsync();  
		}
		private IQueryable<T> ApplySpecifications(ISpecification<T> specification)
		{
			return SpecificationEvulator<T>.GetQuery(_context.Set<T>(), specification);
		}



	}
}
