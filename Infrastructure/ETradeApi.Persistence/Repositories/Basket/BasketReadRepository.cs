using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
{
	public BasketReadRepository(ETradeApiContext context) : base(context)
	{
	}
}
