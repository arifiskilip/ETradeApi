using ETradeApi.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETradeApi.Persistence.CustomeValidation
{
	public class CustomPasswordValidator : IPasswordValidator<AppUser>
	{
		public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
		{
			List<IdentityError> errors = new List<IdentityError>();
			if (password.ToLower().Contains(user.UserName.ToLower()))
			{
				if (!user.Email.ToLower().Contains(user.UserName.ToLower()))
				{
					errors.Add(new IdentityError { Code = "PasswordContainsUsername", Description = "Şifre alanı kullancı adı içeremez." });
				}
			}
			if (password.ToLower().Contains(user.Email.ToLower()))
			{
				errors.Add(new IdentityError { Code = "PasswordContainsEmail", Description = "Şifre alanı email içeremez." });
			}
			///Control
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
