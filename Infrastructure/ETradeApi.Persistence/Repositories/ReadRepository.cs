using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities.Common;
using ETradeApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ETradeApi.Persistence.Repositories
{
	public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
	{
		private readonly ETradeApiContext _context;

		public ReadRepository(ETradeApiContext context)
		{
			_context = context;
		}

		private DbSet<T> Table => _context.Set<T>();

		public IQueryable<T> GetAll(bool tracking = true)
		{
			var query = Table.AsQueryable();
			if (!tracking)
				query = query.AsNoTracking();
			return query;
		}

		public async Task<T> GetByIdAsync(string id, bool tracking = true)
		{
			var query = Table.AsQueryable();
			if (!tracking)
				query = Table.AsNoTracking();
			return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
		}

		public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true)
		{
			var query = Table.AsQueryable();
			if (!tracking)
				query = Table.AsNoTracking();
			return await query.FirstOrDefaultAsync(filter);
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> filter, bool tracking = true)
		{
			var query = Table.Where(filter);
			if (!tracking)
				query = query.AsNoTracking();
			return query;
		}
	}
}
