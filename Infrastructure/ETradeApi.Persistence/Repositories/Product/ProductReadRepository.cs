using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Persistence.Contexts;

namespace ETradeApi.Persistence.Repositories;

public class ProductReadRepository : ReadRepository<Product>,IProductReadRepository
{
	public ProductReadRepository(ETradeApiContext context) : base(context)
	{
	}
}
