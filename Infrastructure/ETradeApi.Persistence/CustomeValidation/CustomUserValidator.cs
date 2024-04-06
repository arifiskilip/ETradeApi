using ETradeApi.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETradeApi.Persistence.CustomeValidation
{
	public class CustomUserValidator : IUserValidator<AppUser>
	{
		public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
		{
			char[] Digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
			List<IdentityError> errors = new List<IdentityError>();
			foreach (var item in Digits)
			{
				if (user.UserName[0] == item)
				{
					errors.Add(new IdentityError { Code = "UserNameContainsDigit", Description = "Kullanıcı adı rakam ile başlayamaz." });
				}
			}
			if (errors.Count == 0)
			{
				return Task.FromResult(IdentityResult.Success);
			}
			else
			{
				return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
			}
		}
	}
}
