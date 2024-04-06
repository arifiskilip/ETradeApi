using ETradeApi.Application.Repositories;
using ETradeApi.Application.Tools.Results;
using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Helpers.FileHelper;
using MediatR;

namespace ETradeApi.Application.Features.Commands.Products.Delete
{
	public class UpdateProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
	{
		private readonly IProductWriteRepository _productWriteRepository;
		private readonly IProductReadRepository _productReadRepository;

		public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
		{
			_productWriteRepository = productWriteRepository;
			_productReadRepository = productReadRepository;
		}

		public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
		{
			Product checkCProduct = await _productReadRepository.GetByIdAsync(request.Id, true);
			if (checkCProduct != null)
			{
				_productWriteRepository.Delete(checkCProduct);
				await _productWriteRepository.SaveAsync();
				FileHelper.Delete(checkCProduct.Images);
				return new()
				{
					Data = new SuccessResult("Silme işlemi başarılı!")
				};
			}
			return new()
			{
				Data = new ErrorResult("İlgili ürün mevcut değil.")
			};
		}
	}
}
