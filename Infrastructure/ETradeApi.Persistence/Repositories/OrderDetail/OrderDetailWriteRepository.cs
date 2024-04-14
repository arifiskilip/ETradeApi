using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class OrderDetailWriteRepository : WriteRepository<OrderDetail>, IOrderDetailWriteRepository
{
	public OrderDetailWriteRepository(ETradeApiContext context) : base(context)
	{
	}
}
