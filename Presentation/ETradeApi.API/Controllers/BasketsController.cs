using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.Features.Commands.Basket.Add;
using ETradeApi.Application.Features.Commands.Basket.Remove;
using ETradeApi.Application.Features.Queries.Basket.GetAllByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETradeApi.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Admin")]
	public class BasketsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public BasketsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody]AddBasketRequest request)
		{
			var result = await _mediator.Send(request);
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllByUserId([FromQuery] GetAllByUserIdRequest request)
		{
			var result = await _mediator.Send(request);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Delete([FromBody]RemoveBasketRequest request)
		{
			var result = await _mediator.Send(request);
			return Ok(result);
		}
	}
}
