using ETradeApi.Application.Features.Commands.AppUsers.RefreshTokenLogin;
using ETradeApi.Application.Features.Commands.AppUsers.Register;
using ETradeApi.Core.Entities.Identity;

namespace ETradeApi.Application.Abstractions.Services
{
	public interface IAuthService
	{
		Task<Dtos.Token> LoginAsync(string usernameOrEmail, string password);
		Task<RefreshTokenLoginCommandResponse> RefreshTokenLoginAsync(string refreshToken);
		Task<RegisterUserCommandResponse> RegisterAsync(RegisterUserCommandRequest request);
		Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
	}
}
