using MediatR;

namespace ETradeApi.Application.Features.Queries.Products.GetAllProduct
{
	public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
	{
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 5;

	}
}
