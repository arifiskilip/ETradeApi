using ETradeApi.Application.Features.Commands.AppUsers.Login;
using ETradeApi.Application.Features.Commands.AppUsers.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeApi.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
		public async Task<IActionResult> Regiser([FromBody]RegisterUserCommandRequest request)
		{
			var result = await _mediator.Send(request);
			if (result.Data.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Data);
		}
		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
		{
			var result = await _mediator.Send(request);
			if (result.Data.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Data);
		}
	}
}
