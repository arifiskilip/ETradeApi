using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class OrderReadRepository : ReadRepository<Order>,IOrderReadRepository
{
	public OrderReadRepository(ETradeApiContext context) : base(context)
	{
	}
}
