using ETradeApi.Application.Tools.Results;
using ETradeApi.Core.Entities;

namespace ETradeApi.Application.Features.Queries.Products.GetByIdProduct
{
	public class GetByIdProductQueryResponse
	{
        public IDataResult<Product> Data { get; set; }
    }
}
