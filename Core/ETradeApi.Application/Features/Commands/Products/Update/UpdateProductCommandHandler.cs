using ETradeApi.Application.Repositories;
using ETradeApi.Core.Entities;
using ETradeApi.Infrastructure.Helpers.FileHelper;
using ETradeApi.Infrastructure.Results;
using MediatR;

namespace ETradeApi.Application.Features.Commands.Products.Update
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
	{
		private readonly IProductWriteRepository _productWriteRepository;
		private readonly IProductReadRepository _productReadRepository;

		public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
		{
			_productWriteRepository = productWriteRepository;
			_productReadRepository = productReadRepository;
		}

		public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
		{
			IDataResult<List<string>> prductImages = new SuccessDataResult<List<string>>();
			try
			{
				var checkProduct = await _productReadRepository.GetByIdAsync(request.Id);
				if (request.Images != null)
				{
					prductImages = FileHelper.Add(request.Images);
					if (prductImages.Success)
					{
						foreach (var item in prductImages.Data)
						{
							checkProduct.Images.Add(item);
						}
						
					}
					else
					{
						return new()
						{
							Data = new ErrorDataResult<Product>(prductImages.Message)
						};
					}
				}
				checkProduct.Name = request.Name;
				checkProduct.Price = request.Price;
				checkProduct.Stock = request.Stock;
				_productWriteRepository.Update(checkProduct);
						await _productWriteRepository.SaveAsync();
				return new()
				{
					Data = new SuccessDataResult<Product>(checkProduct, "Günceleme işlemi başarılı")
				};
			}
			catch (Exception)
			{
				if (prductImages.Data.Count > 0)
				{
					FileHelper.Delete(prductImages.Data);
				}
				return new()
				{
					Data= new ErrorDataResult<Product>("İşlem sırasında hata meydana geldi.")
				};
			}
		}
	}
}
