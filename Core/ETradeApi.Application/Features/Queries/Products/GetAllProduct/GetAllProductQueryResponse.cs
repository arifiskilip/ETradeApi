
using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Pagination;

namespace ETradeApi.Application.Features.Queries.Products.GetAllProduct
{
	public class GetAllProductQueryResponse
	{
		public PaginatedList<Product> Datas { get; set; }
	}
}
