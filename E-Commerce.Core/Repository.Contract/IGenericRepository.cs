using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repository.Contract
{
	public interface IGenericRepository<T> where T : BaseModel
	{
		 Task<IEnumerable<T>> GetAllAsync();
		Task<T?> GetByIdAsync(int id);	
	}
}
