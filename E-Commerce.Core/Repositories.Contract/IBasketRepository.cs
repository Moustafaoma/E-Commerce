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
		Task<Basket?> GetBasketAsync(string basketId, CancellationToken cancellationToken = default);
		Task SaveBasketAsync(Basket basket, TimeSpan? ttl = null, CancellationToken cancellationToken = default);
		Task DeleteBasketAsync(string basketId, CancellationToken cancellationToken = default);
	}
}
