using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETradeApi.Application.Features.Commands.Products.Delete
{
	public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
	{
        public string Id { get; set; }
    }
}
