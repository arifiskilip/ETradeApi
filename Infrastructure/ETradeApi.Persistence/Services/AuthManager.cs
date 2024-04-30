using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.Abstractions.Token;
using ETradeApi.Application.Dtos;
using ETradeApi.Application.Features.Commands.AppUsers.RefreshTokenLogin;
using ETradeApi.Application.Features.Commands.AppUsers.Register;
using ETradeApi.Application.Tools.Results;
using ETradeApi.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETradeApi.Persistence.Services
{
	public class AuthManager : IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;

		public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		public async Task<Token> LoginAsync(string usernameOrEmail, string password)
		{
			var checkUser = await _userManager.FindByNameAsync(usernameOrEmail);
			if (checkUser == null)
			{
				checkUser = await _userManager.FindByEmailAsync(usernameOrEmail);
			}
			if (checkUser != null)
			{
				var result = await _signInManager.CheckPasswordSignInAsync(checkUser, password, false);
				if (result.Succeeded)
				{
					await _userManager.ResetAccessFailedCountAsync(checkUser);
					var token = _tokenService.CreateAccessToken(5, checkUser);
					checkUser.RefreshToken = token.RefreshToken;
					checkUser.RefreshTokenExpiration = token.Expiration;
					await _userManager.UpdateAsync(checkUser);
					return token;
				}
			}
			throw new Exception("Kullanıcı adı veye şifre hatalı.");
		}

		public async Task<RefreshTokenLoginCommandResponse> RefreshTokenLoginAsync(string refreshToken)
		{
			AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
			if (user != null && user?.RefreshTokenExpiration > DateTime.UtcNow)
			{
				Token token = _tokenService.CreateAccessToken(60, user);
				await UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 70);
				return new()
				{
					Data = new SuccessDataResult<Token>(token, "Geçerli token.")
				};
			}
			else
				return new()
				{
					Data = new ErrorDataResult<Token>("Geçersiz token."),
				};
		}

		public async Task<RegisterUserCommandResponse> RegisterAsync(RegisterUserCommandRequest request)
		{
			var checkRepeatedEmail = await _userManager.FindByEmailAsync(request.Email);
			if (checkRepeatedEmail != null)
			{
				return new()
				{
					Data = new ErrorDataResult<Token>("Bu e-posta zaten kullanılıyor.")
				};
			}
			AppUser user = new()
			{
				Id = Guid.NewGuid().ToString(),
				UserName = request.UserName,
				Email = request.Email,
				FullName = request.FullName
			};
			var result = await _userManager.CreateAsync(user, request.Password);
			if (result.Succeeded)
			{
				var token = _tokenService.CreateAccessToken(5000, user);
				return new()
				{

					Data = new SuccessDataResult<Token>(token, "Kayıt işlemi başarılıi!")
				};
			}
			else
			{
				return new()
				{
					Data = new ErrorDataResult<Token>(result.Errors.Select(x => x.Description).First())
				};
			}
		}

		public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
		{
			if (user != null)
			{
				user.RefreshToken = refreshToken;
				user.RefreshTokenExpiration = accessTokenDate.AddMinutes(addOnAccessTokenDate);
				await _userManager.UpdateAsync(user);
			}
			else
				throw new Exception("Kullanıcı bulunamadı!");
		}
	}
}
