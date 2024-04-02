
using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Pagination;
using ETradeApi.Infrastructure.Results;

namespace ETradeApi.Application.Features.Queries.Products.GetAllProduct
{
	public class GetAllProductQueryResponse
	{
		public PaginatedList<Product> Products { get; set; }
	}
}
