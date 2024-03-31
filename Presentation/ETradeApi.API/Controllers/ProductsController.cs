using ETradeApi.API.Models;
using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Helpers.FileHelper;
using ETradeApi.Infrastructure.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public IActionResult GetAll(int pageIndex=1, int pageSize=5)
		{
			var productsQuery = _productReadRepository.GetAll(false);

		    var result = PaginatedList<Product>.Create(productsQuery, pageIndex, pageSize);

			return Ok(result);
		}
		[HttpGet]
		public async Task<IActionResult> GetById(string id)
		{
			Product checkCProduct = await _productReadRepository.GetByIdAsync(id,false);
			if (checkCProduct != null)
			{
				return Ok(checkCProduct);
			}
			return BadRequest("İlgili ürün mevcut değil.");
		}
		[HttpPost]
		public async Task<IActionResult> Add(Product product)
		{
			await _productWriteRepository.AddAsync(product);
			await _productWriteRepository.SaveAsync();
			return Ok(_productReadRepository.GetAll());
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(string id)
		{
			Product checkCProduct = await _productReadRepository.GetByIdAsync(id, true);
			if (checkCProduct != null)
			{
				_productWriteRepository.Delete(checkCProduct);
				await _productWriteRepository.SaveAsync();
				FileHelper.Delete(checkCProduct.Images);
				return Ok();
			}
			return BadRequest("İlgili ürün mevcut değil.");
		}

		[HttpPost]
		public async Task<IActionResult> Update([FromForm]ProductUpdateModel product)
		{
			ProductManager productManager = new ProductManager(this._productWriteRepository, this._productReadRepository);
			var result = await productManager.Update(product);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> AddV2([FromForm]ProductCreateModel model)
		{
			ProductManager productManager = new ProductManager(this._productWriteRepository,this._productReadRepository);
			var result = await productManager.AddAsync(model);
			return Ok(result);
		}
	}
}
