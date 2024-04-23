using ETradeApi.Core.Entities;

namespace ETradeApi.Application.Abstractions.Services
{
	public interface IBasketService
	{
		Task<Basket> AddAsync(Basket basket);
		Task DeleteAsync(string id,int? quantity=null);
		Task<List<Basket>> GetAllByUserIdAsync();
	}
}
