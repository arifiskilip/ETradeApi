using ETradeApi.Core.Entities.Common;

namespace ETradeApi.Infrastructure.Pagination
{
	public class PaginatedList<T> where T:BaseEntity 
	{
		public IReadOnlyList<T> Items { get; }
		public PaginationInfo Pagination { get; }

		private PaginatedList(IReadOnlyList<T> items, PaginationInfo pagination)
		{
			Items = items;
			Pagination = pagination;
		}

		public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (pageIndex < 1)
			{
				throw new ArgumentException("Page index must be greater than or equal to 1", nameof(pageIndex));
			}

			if (pageSize < 1)
			{
				throw new ArgumentException("Page size must be greater than or equal to 1", nameof(pageSize));
			}

			var totalItems = source.Count();
			var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
			var pageItems = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderByDescending(x=>x.CreatedDate).ToList();

			var paginationInfo = new PaginationInfo
			{
				PageIndex = pageIndex,
				PageSize = pageSize,
				TotalItems = totalItems,
				TotalPages = totalPages
			};

			return new PaginatedList<T>(pageItems, paginationInfo);
		}
	}
}
