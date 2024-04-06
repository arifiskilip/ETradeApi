using Microsoft.AspNetCore.Identity;

namespace ETradeApi.Persistence.CustomeValidation
{
	public class CustomIdentityErrorDescriber : IdentityErrorDescriber
	{
		public override IdentityError PasswordTooShort(int length)
		{
			return new IdentityError
			{
				Code = "PasswordToShort",
				Description = $"Şifre alanınız {length} karakter olmalıdır."
			};
		}
		public override IdentityError PasswordRequiresUpper()
		{
			return new IdentityError
			{
				Code = "PasswordRequiresUpper",
				Description = "Şifre alanı en az 1 adet büyük harf içermelidir."
			};
		}

		public override IdentityError PasswordRequiresLower()
		{
			return new IdentityError
			{
				Code = "PasswordRequiresLower",
				Description = "Şifre alanı en az 1 adet küçük harf içermelidir."
			};
		}

		public override IdentityError PasswordRequiresNonAlphanumeric()
		{
			return new IdentityError
			{
				Code = "PasswordRequiresNonAlphanumeric",
				Description = "Şifre alanı en az 1 adet sembol içermelidir."
			};
		}

		public override IdentityError PasswordRequiresDigit()
		{
			return new IdentityError
			{
				Code = "PasswordRequiresNonAlphanumeric",
				Description = "Şifre alanı ['0' - '9'] arasında bir sayı içermelidir."
			};
		}

		public override IdentityError DuplicateUserName(string userName)
		{
			return new IdentityError
			{
				Code = "DublicateUserName",
				Description = $"Bu {userName} kullanılmaktadır."
			};
		}

		public override IdentityError DuplicateEmail(string email)
		{
			return new IdentityError
			{
				Code = "DuplicateEmail",
				Description = $"Bu {email} kullanılmaktadır."
			};
		}

		public override IdentityError DuplicateRoleName(string role)
		{
			return new IdentityError
			{
				Code = "DuplicateRoleName",
				Description = $"Bu {role} kullanılmaktadır."
			}; ;
		}
	}
}
