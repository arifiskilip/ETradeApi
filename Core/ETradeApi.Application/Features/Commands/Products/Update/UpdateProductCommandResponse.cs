using ETradeApi.Application.Tools.Results;
using ETradeApi.Core.Entities;

namespace ETradeApi.Application.Features.Commands.Products.Update
{
	public class UpdateProductCommandResponse
	{
        public IDataResult<Product> Data { get; set; }
    }
}
