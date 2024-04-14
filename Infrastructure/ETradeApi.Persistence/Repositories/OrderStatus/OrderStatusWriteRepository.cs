using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class OrderStatusWriteRepository : WriteRepository<OrderStatus>, IOrderStatusWriteRepository
{
	public OrderStatusWriteRepository(ETradeApiContext context) : base(context)
	{
	}
}
