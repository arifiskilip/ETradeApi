using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Results;

namespace ETradeApi.Application.Features.Queries.Products.GetByIdProduct
{
	public class GetByIdProductQueryResponse
	{
        public IDataResult<Product> Data { get; set; }
    }
}
