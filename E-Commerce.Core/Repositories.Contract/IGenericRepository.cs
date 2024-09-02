using E_Commerce.Core.Models;
using E_Commerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repository.Contract
{
	public interface IGenericRepository<T> where T : BaseModel
	{
		 Task<IReadOnlyList<T>> GetAllAsync();
		Task<T?> GetByIdAsync(int id);
		public  Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> specification);
		 Task<T?> GetByIdWithSpecAsync(ISpecification<T> specification);
		 Task<int> GetCountAsync(ISpecification<T> specification);
	}
}
