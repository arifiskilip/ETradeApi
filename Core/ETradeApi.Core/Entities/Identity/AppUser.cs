using Microsoft.AspNetCore.Identity;

namespace ETradeApi.Core.Entities.Identity
{
	public class AppUser : IdentityUser<string>
	{
        public string FullName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }


		public ICollection<Basket> Baskets { get; set; }
	}
}
