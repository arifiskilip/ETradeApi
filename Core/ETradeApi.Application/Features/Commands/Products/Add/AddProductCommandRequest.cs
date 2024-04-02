using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETradeApi.Application.Features.Commands.Products.Add
{
	public class AddProductCommandRequest : IRequest<AddProductCommandResponse>
	{
		public string Name { get; set; }
		public double Price { get; set; }
		public int Stock { get; set; }
		public IFormFile[] Images { get; set; }
	}
}
