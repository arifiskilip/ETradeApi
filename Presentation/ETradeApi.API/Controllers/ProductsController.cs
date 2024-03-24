using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ETradeApi.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IProductWriteRepository _productWriteRepository;

		public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
		}
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var entity = await _productReadRepository.GetByIdAsync("b415bb1a-09cd-4c42-9964-4b54320ee288");
			entity.Name = " Arif3";
			await _productWriteRepository.SaveAsync();
			return Ok(_productReadRepository.GetAll());
		}
		[HttpGet]
		public async Task<IActionResult> Get2()
		{
			var entities = new List<Product>()
			{
				new()
				{
					Name="Deneme 1",
					Price=100,
					Stock=50
				},
				new()
				{
					Name="Deneme 2",
					Price=100,
					Stock=50
				}
			};
			await _productWriteRepository.AddAsync(entities);
			await _productWriteRepository.SaveAsync();
			return Ok(_productReadRepository.GetAll());
		}
	}
}
