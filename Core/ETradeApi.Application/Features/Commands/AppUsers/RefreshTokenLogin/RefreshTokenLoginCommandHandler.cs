using ETradeApi.Application.Abstractions.Services;
using MediatR;

namespace ETradeApi.Application.Features.Commands.AppUsers.RefreshTokenLogin
{
	public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
	{
		private readonly IAuthService _authService;

		public RefreshTokenLoginCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
		{
			var result = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
			return result;
		}
	}
}
