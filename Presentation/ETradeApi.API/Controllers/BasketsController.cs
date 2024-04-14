using ETradeApi.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETradeApi.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]
	public class BasketsController : ControllerBase
	{
		private readonly IBasketService _basketService;

		public BasketsController(IBasketService basketService)
		{
			_basketService = basketService;
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody]BasketDto dto)
		{
			var result = await _basketService.AddAsync(new()
			{
				ProductId = Guid.Parse(dto.ProductId),
				Quantity = dto.Quantity
			});
			return Ok(result);
		}
	}

	public class BasketDto
	{
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
