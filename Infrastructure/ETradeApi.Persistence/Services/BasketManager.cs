using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETradeApi.Persistence.Services
{
	public class BasketManager : IBasketService
	{
		private readonly IBasketWriteRepository _basketWriteRepository;
		private readonly IBasketReadRepository _basketReadRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<AppUser> _userManager;

		public BasketManager(IBasketWriteRepository basketWriteRepository, IBasketReadRepository basketReadRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
		{
			_basketWriteRepository = basketWriteRepository;
			_basketReadRepository = basketReadRepository;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}

		public async Task<Basket> AddAsync(Basket basket)
		{
			var checkProduct = await _basketReadRepository.GetSingleAsync(x=> x.ProductId == basket.ProductId);
			if (checkProduct != null)
			{
				checkProduct.Quantity += basket.Quantity;
				await _basketWriteRepository.SaveAsync();
				return checkProduct;
			}
			basket.AppUserId = await GetUserId();
			var addedBasket = await _basketWriteRepository.AddAsync(basket);
			await _basketWriteRepository.SaveAsync();
			return addedBasket.Entity;
		}

		public Task DeleteAsync(string id)
		{
			throw new NotImplementedException();
		}


		private async Task<string> GetUserId()
		{
			var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
			AppUser user = await _userManager.FindByNameAsync(userName);

			if (user != null)
			{
				return user.Id;
			}
			throw new Exception("Lütfen sisteme giriş yapın!");
		}
	}
}
