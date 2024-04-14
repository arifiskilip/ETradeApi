using ETradeApi.Application.Abstractions.Services;
using ETradeApi.Application.Abstractions.Token;
using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities.Identity;
using ETradeApi.Persistence.Contexts;
using ETradeApi.Persistence.CustomeValidation;
using ETradeApi.Persistence.Repositories;
using ETradeApi.Persistence.Services;
using ETradeApi.Persistence.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeApi.Persistence;

public static class ServiceRegistration
{
	public static void AddPersistenceServices(this IServiceCollection services)
	{
		services.AddDbContext<ETradeApiContext>(options => options.UseSqlServer(Connection.GetConnection));
		services.AddIdentity<AppUser, AppRole>()
			.AddPasswordValidator<CustomPasswordValidator>()
			.AddUserValidator<CustomUserValidator>()
			.AddErrorDescriber<CustomIdentityErrorDescriber>()
			.AddEntityFrameworkStores<ETradeApiContext>()
			.AddDefaultTokenProviders();

		//IoC

		services.AddScoped<IProductReadRepository, ProductReadRepository>();
		services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
		services.AddScoped<IOrderReadRepository,
			OrderReadRepository>();
		services.AddScoped<IOrderWriteRepository,
			OrderWriteRepository>();


		services.AddScoped<IAuthService, AuthManager>();
	}
}
