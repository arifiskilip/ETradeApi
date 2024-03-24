using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class OrderWriteRepository : WriteRepository<Order>,IOrderWriteRepository
{
	public OrderWriteRepository(ETradeApiContext context) : base(context)
	{
	}
}
