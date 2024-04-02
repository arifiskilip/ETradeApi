using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Results;
using MediatR;

namespace ETradeApi.Application.Features.Queries.Products.GetByIdProduct
{
	public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
	{
		private readonly IProductReadRepository _productReadRepository;

		public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
		{
			_productReadRepository = productReadRepository;
		}

		public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
		{
			var checkCProduct = await _productReadRepository.GetByIdAsync(request.Id, false);
			if (checkCProduct != null)
			{
				return new()
				{
					Data = new SuccessDataResult<Product>(checkCProduct, "Başarılı!")

				};
			}
			return new()
			{
				Data = new ErrorDataResult<Product>(null, $"Id:{request.Id}'ye sahip ürün mevcut değil!")
			};
		}
	}
}