using ETradeApi.Application.Features.Commands.Products.Add;
using ETradeApi.Application.Features.Commands.Products.Delete;
using ETradeApi.Application.Features.Commands.Products.DeleteByImagePath;
using ETradeApi.Application.Features.Commands.Products.Update;
using ETradeApi.Application.Features.Queries.Products.GetAllProduct;
using ETradeApi.Application.Features.Queries.Products.GetByIdProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETradeApi.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<ProductsController> _logger;

		public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery]GetAllProductQueryRequest request)
		{
			_logger.Log(LogLevel.Error,"Test deneme 123.");
			var result = await _mediator.Send(request);
			return Ok(result);
		}
		[HttpGet]
		public async Task<IActionResult> GetById([FromQuery]GetByIdProductQueryRequest request)
		{
			var result = await _mediator.Send(request);
			if (result.Data.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Data);
		}
		[HttpPost]
		[Authorize(AuthenticationSchemes = "Admin")]
		public async Task<IActionResult> Add([FromForm]AddProductCommandRequest request)
		{
			var result = await _mediator.Send(request);
			if (result.Data.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Data);
		}
		[HttpDelete]
		[Authorize(AuthenticationSchemes = "Admin")]
		public async Task<IActionResult> Delete([FromQuery]DeleteProductCommandRequest request)
		{
			var result = await _mediator.Send(request);
			if (result.Data.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Data);
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = "Admin")]
		public async Task<IActionResult> Update([FromForm]UpdateProductCommandRequest request)
		{
			var result = await _mediator.Send(request);
			if (result.Data.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Data);
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes = "Admin")]
		public async Task<IActionResult> UrlByDeleteImage([FromQuery]DeleteByImagePathRequest request)
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
