using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Results;

namespace ETradeApi.Application.Features.Commands.Products.Update
{
	public class UpdateProductCommandResponse
	{
        public IDataResult<Product> Data { get; set; }
    }
}
