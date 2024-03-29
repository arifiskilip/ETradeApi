using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ETradeApi.Application.Repositories
{
	public interface IWriteRepository<T> : IRepository<T> where T : class
	{
		Task<EntityEntry<T>> AddAsync(T entity);
		Task<bool> AddAsync(List<T> entities);
		bool Update(T entity);
		bool Delete(T entity);
		bool Delete(string id);
		Task<int> SaveAsync();
		
		
	}
}
