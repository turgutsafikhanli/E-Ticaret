using E_Ticaret.Application.DTOs.OrderProductDtos;
using FluentValidation;

namespace E_Ticaret.Application.Validations.OrderProductValidations;

public class OrderProductCreateDtoValidator : AbstractValidator<OrderProductCreateDto>
{
    public OrderProductCreateDtoValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Miqdar 0-dan böyük olmalıdır.")
            .LessThanOrEqualTo(10000).WithMessage("Miqdar 10000-dən çox olmamalıdır.");
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId boş ola bilməz.")
            .NotNull().WithMessage("ProductId null ola bilməz.");
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId boş ola bilməz.")
            .NotNull().WithMessage("OrderId null ola bilməz.");
    }
}
