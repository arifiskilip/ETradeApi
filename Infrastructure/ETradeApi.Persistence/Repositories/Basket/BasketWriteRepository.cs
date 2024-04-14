using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
{
	public BasketWriteRepository(ETradeApiContext context) : base(context)
	{
	}
}
