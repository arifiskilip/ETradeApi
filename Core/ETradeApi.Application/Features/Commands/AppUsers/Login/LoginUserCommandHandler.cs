using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.Dtos;
using ETradeApi.Application.Tools.Results;
using MediatR;

namespace ETradeApi.Application.Features.Commands.AppUsers.Login
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
	{
		private readonly IAuthService _authService;

		public LoginUserCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
		{
			var token = await _authService.LoginAsync(request.UserNameOrEmail,request.Password);
			if (token != null)
			{
				return new()
				{
					Data = new SuccessDataResult<Token>(token, "Giriş işlemi başarılı!")
				};
			}
			return new()
			{
				Data = new ErrorDataResult<Token>("Kullanıcı adı veya şifre hatalı!")
			};
		}
	}
}
