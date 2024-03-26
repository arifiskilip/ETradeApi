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
		public async Task<IActionResult> GetAll()
		{
			return Ok(_productReadRepository.GetAll().OrderByDescending(x=> x.CreatedDate));
		}
		[HttpPost]
		public async Task<IActionResult> Add(Product product)
		{
			await _productWriteRepository.AddAsync(product);
			await _productWriteRepository.SaveAsync();
			return Ok(_productReadRepository.GetAll());
		}
	}
}
