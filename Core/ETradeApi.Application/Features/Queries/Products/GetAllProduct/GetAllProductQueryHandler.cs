using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Pagination;
using ETradeApi.Infrastructure.Results;
using MediatR;

namespace ETradeApi.Application.Features.Queries.Products.GetAllProduct;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
	private readonly IProductReadRepository _productReadRepository;

	public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
	{
		_productReadRepository = productReadRepository;
	}

	public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
	{
		var productsQuery = _productReadRepository.GetAll(false);

		var result = PaginatedList<Product>.Create(productsQuery, request.pageIndex, request.pageSize);

		return new()
		{
			Products = result,
		};
	}
}


