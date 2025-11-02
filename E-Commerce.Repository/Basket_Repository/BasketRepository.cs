using E_Commerce.Core.Models;
using E_Commerce.Core.Repositories.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Basket_Repository
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;

		public BasketRepository(IConnectionMultiplexer redis)
		{
			_database = redis.GetDatabase();
		}
		public async Task<Basket?> GetBasketAsync(string basketId)
		{
			var data =  await _database.StringGetAsync(basketId);
			if (data.IsNullOrEmpty) return null;

			return JsonSerializer.Deserialize<Basket>(data);
		}
		public async Task<Basket?> SaveBasketAsync(Basket basket)
		{
			var createdOrUpdated = await _database.StringSetAsync(
			   basket.BasketId,
			   JsonSerializer.Serialize(basket),
			   TimeSpan.FromDays(30) 
		   );

			if (!createdOrUpdated) return null;

			return await GetBasketAsync(basket.BasketId);
		}
		public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _database.KeyDeleteAsync(basketId);
		}


	}
}
