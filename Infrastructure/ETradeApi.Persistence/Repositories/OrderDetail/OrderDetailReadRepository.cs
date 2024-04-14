using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class OrderDetailReadRepository : ReadRepository<OrderDetail>, IOrderDetailReadRepository
{
	public OrderDetailReadRepository(ETradeApiContext context) : base(context)
	{
	}
}
