using ETradeApi.Core.Entities.Common;
using System.Linq.Expressions;

namespace ETradeApi.Application.Repositories
{
	public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
	{
		IQueryable<T> GetAll(bool tracking=true);
		IQueryable<T> Where(Expression<Func<T,bool>> filter, bool tracking = true);
		Task<T> GetSingleAsync(Expression<Func<T,bool>> filter, bool tracking = true);
		Task<T> GetByIdAsync(string id, bool tracking = true);
	}
}
