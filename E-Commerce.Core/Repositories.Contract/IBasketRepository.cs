using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace E_Commerce.Core.Repositories.Contract
{
	public interface IBasketRepository
	{
		Task<Basket?> GetBasketAsync(string basketId);
		Task<Basket?> SaveBasketAsync(Basket basket);
		Task<bool> DeleteBasketAsync(string basketId);
	}
}
