using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Core.Entities.Identity;
using ETradeApi.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
			Basket test = new Basket()
			{
				AppUserId = await GetUserId(),
				ProductId = basket.ProductId,
				Quantity = 1
			};
			//basket.ProductId = basket.ProductId;
			//basket.AppUserId = await GetUserId();
			await _basketWriteRepository.AddAsync(test);
			await _basketWriteRepository.SaveAsync();
			return basket;
		}

		public async Task DeleteAsync(string id, int? quantity = null)
		{
			var checkBasket = await _basketReadRepository.GetByIdAsync(id);
			if (checkBasket != null)
			{
				if (quantity != null)
				{
					if (checkBasket.Quantity ==1)
					{
						_basketWriteRepository.Delete(checkBasket);
					}
					else
					{
						checkBasket.Quantity -= (int)quantity;
					}
				}
				else
				{
					_basketWriteRepository.Delete(checkBasket);
				}
				await _basketWriteRepository.SaveAsync();
			}
			else
			{
				throw new Exception("İlgili sepet bulunamadı.");
			}
		}

		public async Task<List<Basket>> GetAllByUserIdAsync()
		{
			var appUserId = await GetUserId();
			var result = await _basketReadRepository
				.Where(filter: x => x.AppUserId == appUserId, tracking:false)
				.Include(x=>x.Product)
				.Select(x=> new Basket()
				{
					Id = x.Id,
					AppUserId = x.AppUserId,
					ProductId = x.ProductId,
					Quantity = x.Quantity,
					CreatedDate = x.CreatedDate,
					UpdatedDate = x.UpdatedDate,
					Product = x.Product
				})
				.ToListAsync();
			return result;
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
