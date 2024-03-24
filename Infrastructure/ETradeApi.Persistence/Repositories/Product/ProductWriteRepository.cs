using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class ProductWriteRepository : WriteRepository<Product>,IProductWriteRepository
{
	public ProductWriteRepository(ETradeApiContext context) : base(context)
	{
	}
}
