using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.Tools.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.Features.Commands.Basket.Remove
{
	public class RemoveBasketHandler : IRequestHandler<RemoveBasketRequest, RemoveBasketResponse>
	{
		private readonly IBasketService _basketService;

		public RemoveBasketHandler(IBasketService basketService)
		{
			_basketService = basketService;
		}

		public async Task<RemoveBasketResponse> Handle(RemoveBasketRequest request, CancellationToken cancellationToken)
		{
			 await _basketService.DeleteAsync(request.ProductId, request.Quantity);

			return new()
			{
				Result = new SuccessResult("Silm işlemi başarılı!")
			};
		}
	}
}
