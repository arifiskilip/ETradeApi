using ETradeApi.Application.Abstractions.Token;
using ETradeApi.Application.Dtos;
using ETradeApi.Application.Tools.Results;
using ETradeApi.Core.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETradeApi.Application.Features.Commands.AppUsers.Login
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;

		public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
		{
			var checkUser = await _userManager.FindByNameAsync(request.UserNameOrEmail);
			if (checkUser == null)
			{
				checkUser = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
			}
			if (checkUser != null)
			{
				var result = await _signInManager.CheckPasswordSignInAsync(checkUser, request.Password, false);
				if (result.Succeeded)
				{
					await _userManager.ResetAccessFailedCountAsync(checkUser);
					return new()
					{
						Data = new SuccessDataResult<Token>(_tokenService.CreateAccessToken(5, checkUser), "Giriş işlemi başarılı!")
					};
				}
			}
			return new()
			{
				Data = new ErrorDataResult<Token>("Kullanıcı adı veye şifre hatalı.")
			};
		}
	}
}
