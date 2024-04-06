using ETradeApi.Application.Tools.Results;
using ETradeApi.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ETradeApi.Application.Features.Commands.Products.Add
{
	public class AddProductCommandResponse
	{
		public IDataResult<Product> Data { get; set; } = null;
    }

	public class AddProductErrorResponse : AddProductCommandResponse
	{
		public List<IdentityError> Errors { get; set; }
	}
}
