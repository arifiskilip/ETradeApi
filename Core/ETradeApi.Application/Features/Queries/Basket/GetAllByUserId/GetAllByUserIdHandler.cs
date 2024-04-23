using ETradeApi.Application.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETradeApi.Application.Features.Queries.Basket.GetAllByUserId
{
	public class GetAllByUserIdHandler : IRequestHandler<GetAllByUserIdRequest, GetAllByUserIdResponse>
	{
		private readonly IBasketService _basketService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public GetAllByUserIdHandler(IBasketService basketService, IHttpContextAccessor httpContextAccessor)
		{
			_basketService = basketService;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<GetAllByUserIdResponse> Handle(GetAllByUserIdRequest request, CancellationToken cancellationToken)
		{
			var result = await _basketService.GetAllByUserIdAsync();

			return new()
			{
				Baskets = result,
			};
		}
	}
}
