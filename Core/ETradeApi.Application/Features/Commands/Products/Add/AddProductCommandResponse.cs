using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Results;

namespace ETradeApi.Application.Features.Commands.Products.Add
{
	public class AddProductCommandResponse
	{
        public IDataResult<Product> Data { get; set; }
    }
}
