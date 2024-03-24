using ETradeApi.Core.Entities;
using ETradeApi.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETradeApi.Persistence.Contexts;

public class ETradeApiContext : DbContext
{
	public ETradeApiContext(DbContextOptions options) : base(options)
	{
	}

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }


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
