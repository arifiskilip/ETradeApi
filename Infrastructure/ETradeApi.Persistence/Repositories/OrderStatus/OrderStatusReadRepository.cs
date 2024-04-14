using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class OrderStatusReadRepository : ReadRepository<OrderStatus>, IOrderStatusReadRepository
{
	public OrderStatusReadRepository(ETradeApiContext context) : base(context)
	{
	}
}
