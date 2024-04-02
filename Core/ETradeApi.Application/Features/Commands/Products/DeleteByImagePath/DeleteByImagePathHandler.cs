using ETradeApi.Application.Repositories;
using ETradeApi.Infrastructure.Helpers.FileHelper;
using ETradeApi.Infrastructure.Results;
using MediatR;

namespace ETradeApi.Application.Features.Commands.Products.DeleteByImagePath
{
	public class DeleteByImagePathHandler : IRequestHandler<DeleteByImagePathRequest, DeleteByImagePathResponse>
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IProductWriteRepository _productWriteRepository;

		public DeleteByImagePathHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
		}

		public async Task<DeleteByImagePathResponse> Handle(DeleteByImagePathRequest request, CancellationToken cancellationToken)
		{
			var checkProduct = await _productReadRepository.GetByIdAsync(request.ProductId);
			if (checkProduct != null)
			{
				if (checkProduct.Images.Any(x => x == request.ImagePath))
				{
					checkProduct.Images.Remove(request.ImagePath);
					await _productWriteRepository.SaveAsync();
					var result = FileHelper.Delete(request.ImagePath);
					if (result.Success)
					{
						return new()
						{
							Data = result
						};
					}
					return new()
					{
						Data = result
					};
				}
			}
			return new()
			{
				Data = new ErrorResult("Silme işlemi sırasıda hata meydana geldi!")
			};
		}
	}
}
