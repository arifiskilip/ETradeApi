using Microsoft.AspNetCore.Identity;

namespace ETradeApi.Core.Entities.Identity
{
	public class AppUser : IdentityUser<string>
	{
        public string FullName { get; set; }
    }
}
