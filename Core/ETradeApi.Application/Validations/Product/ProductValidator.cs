
using ETradeApi.Core.Entities;
using FluentValidation;

namespace ETradeApi.Application.Validations
{
	public class ProductValidator : AbstractValidator<Product>
	{
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(3).MaximumLength(20).WithName("Ürün Adı");
            RuleFor(x=> x.Price).NotEmpty().NotNull()
                .GreaterThan(0).LessThan(1000).WithName("Fiyat");
			RuleFor(x => x.Stock).NotEmpty().NotNull()
				.GreaterThan(0).LessThan(500).WithName("Stok alanı");

		}
    }
}
