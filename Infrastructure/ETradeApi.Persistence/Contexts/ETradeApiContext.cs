using ETradeApi.Core.Entities;
using ETradeApi.Core.Entities.Common;
using ETradeApi.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETradeApi.Persistence.Contexts;

public class ETradeApiContext : IdentityDbContext<AppUser,AppRole,string>
{
	public ETradeApiContext(DbContextOptions options) : base(options)
	{
	}

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
   


	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var entities = ChangeTracker.Entries<BaseEntity>();
		foreach (var entity in entities)
		{
			if (entity.State == EntityState.Modified)
			{
				entity.Entity.UpdatedDate = DateTime.Now;
			}
		}
		return base.SaveChangesAsync(cancellationToken);
	}
}
