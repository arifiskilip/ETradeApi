using ETradeApi.Application.Repositories;
using ETradeApi.Persistence.Contexts;
using ETradeApi.Persistence.Repositories;
using ETradeApi.Persistence.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETradeApi.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ETradeApiContext>(options => options.UseSqlServer(Connection.GetConnection));

        //IoC

        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        services.AddScoped<IOrderReadRepository,
            OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository,
            OrderWriteRepository>();
    }
}
