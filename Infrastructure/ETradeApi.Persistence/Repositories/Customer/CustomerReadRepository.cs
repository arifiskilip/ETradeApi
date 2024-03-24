using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class CustomerReadRepository : ReadRepository<Customer>,ICustomerReadRepository
{
	public CustomerReadRepository(ETradeApiContext context) : base(context)
	{
	}
}
