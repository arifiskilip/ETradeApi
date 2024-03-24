using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class CustomerWriteRepository : WriteRepository<Customer>,ICustomerWriteRepository
{
	public CustomerWriteRepository(ETradeApiContext context) : base(context)
	{
	}
}
