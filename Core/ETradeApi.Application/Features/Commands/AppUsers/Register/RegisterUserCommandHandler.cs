using ETradeApi.Application.Abstractions.Services;
using MediatR;

namespace ETradeApi.Application.Features.Commands.AppUsers.Register
{
	public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
	{
		private readonly IAuthService _authService;

		public RegisterUserCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
		{
			var result = await _authService.RegisterAsync(request);
			return result;
		}
	}
}
