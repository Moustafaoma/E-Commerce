using E_Commerce.Core.Models;
using E_Commerce.Core.Repository.Contract;
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
        public async Task <IEnumerable<T>> GetAllAsync()
		{
			//Implemented with Specification pattern
			if (typeof(T) == typeof(Product))
			{
				return (IEnumerable<T>)await _context.Set<Product>()
					.Include(p => p.Brand)
					.Include(p => p.Category)
					.AsNoTracking()
					.ToListAsync();
			}
			return await _context.Set<T>().AsNoTracking().ToListAsync();
			 
		}
		

		public async Task<T?> GetByIdAsync(int id)
		{
			if(typeof(T) == typeof(Product))
			{
				return  await _context.Set<Product>().Include(p=>p.Brand).Include(p=>p.Category).FirstOrDefaultAsync(p=>p.Id==id) as T;
			}
		 	return await _context.Set<T>().FindAsync(id);
		}

	}
}
