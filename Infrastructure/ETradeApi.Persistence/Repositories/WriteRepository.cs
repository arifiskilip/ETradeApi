using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities.Common;
using ETradeApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ETradeApi.Persistence.Repositories
{
	public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
	{
		private readonly ETradeApiContext _context;
		
		public WriteRepository(ETradeApiContext context)
		{
			_context = context;
		}
		private DbSet<T> Table => _context.Set<T>();
		public async Task<bool> AddAsync(T entity)
		{
			EntityEntry<T> entityEntry = await Table.AddAsync(entity);
			return entityEntry.State == EntityState.Added;
		}

		public async Task<bool> AddAsync(List<T> entities)
		{
			await Table.AddRangeAsync(entities);
			return true;
		}

		public bool Delete(T entity)
		{
			EntityEntry<T> entityEntry = Table.Remove(entity);
			return entityEntry.State == EntityState.Deleted;
		}

		public bool Delete(string id)
		{
			T model = Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id)).Result;
			return Delete(model);
		}

		public async Task<int> SaveAsync()
		{
		 	return await _context.SaveChangesAsync();
		}

		public bool Update(T entity)
		{
			EntityEntry entityEntry = Table.Update(entity);
			return entityEntry.State == EntityState.Modified;
		}
	}
}
