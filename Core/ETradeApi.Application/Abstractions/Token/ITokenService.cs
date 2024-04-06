using ETradeApi.Core.Entities.Identity;

namespace ETradeApi.Application.Abstractions.Token
{
	public interface ITokenService
	{
		Dtos.Token CreateAccessToken(int minute, AppUser appUser);
		string CreateRefreshToken();
	}
}
