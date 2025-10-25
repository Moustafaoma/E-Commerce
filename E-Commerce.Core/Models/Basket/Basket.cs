using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models
{
	public class Basket
	{
		public string BasketId { get; set; } = default!; 
		public List<BasketItem> Items { get; set; } = new List<BasketItem>();
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	}
}
