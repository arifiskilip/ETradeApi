using ETradeApi.Application.Abstractions.Token;
using ETradeApi.Core.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ETradeApi.Infrastructure.Services.Token
{
	public class TokenManager : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenManager(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public Application.Dtos.Token CreateAccessToken(int minute, AppUser appUser)
		{
			Application.Dtos.Token token = new();

			//Security Key'in simetriğini alıyoruz.
			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

			//Şifrelenmiş kimliği oluşturuyoruz.
			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

			//Oluşturulacak token ayarlarını veriyoruz.
			token.Expiration = DateTime.UtcNow.AddMinutes(minute);
			JwtSecurityToken securityToken = new(
				audience: _configuration["Token:Audience"],
				issuer: _configuration["Token:Issuer"],
				expires: token.Expiration,
				notBefore: DateTime.UtcNow,
				signingCredentials: signingCredentials,
				claims: new List<Claim> { new(ClaimTypes.Name, appUser.UserName) }
				);

			//Token oluşturucu sınıfından bir örnek alalım.
			JwtSecurityTokenHandler tokenHandler = new();
			token.AccessToken = tokenHandler.WriteToken(securityToken);

			//string refreshToken = CreateRefreshToken();

			//token.RefreshToken = CreateRefreshToken();
			return token;

		}

		public string CreateRefreshToken()
		{
			throw new NotImplementedException();
		}
	}
}
