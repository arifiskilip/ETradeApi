using ETradeApi.Application.Abstractions.Token;
using ETradeApi.Application.Dtos;
using ETradeApi.Application.Tools.Results;
using ETradeApi.Core.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETradeApi.Application.Features.Commands.AppUsers.Register
{
	public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly ITokenService _tokenService;

		public RegisterUserCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_tokenService = tokenService;
		}

		public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
		{
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
					Data = new ErrorDataResult<Token>( result.Errors.Select(x => x.Description).First())
				};
			}
		}
	}
}
