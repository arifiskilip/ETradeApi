using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETradeApi.Application.Features.Commands.Products.Update
{
	public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public int Stock { get; set; }
		public IFormFile[]? Images { get; set; }
	}
}
