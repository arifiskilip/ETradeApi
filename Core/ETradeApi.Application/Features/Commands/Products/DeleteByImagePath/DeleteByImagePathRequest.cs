using MediatR;

namespace ETradeApi.Application.Features.Commands.Products.DeleteByImagePath
{
	public class DeleteByImagePathRequest : IRequest<DeleteByImagePathResponse>
	{
		public string ImagePath { get; set; }
		public string ProductId { get; set; }
	}
}
