using E_Ticaret.Application.DTOs.OrderProductDtos;
using FluentValidation;

namespace E_Ticaret.Application.Validations.OrderProductValidations;

public class OrderProductGetDtoValidator : AbstractValidator<OrderProductGetDto>
{
    public OrderProductGetDtoValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId boş ola bilməz.")
            .NotNull().WithMessage("OrderId null ola bilməz.");
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId boş ola bilməz.")
            .NotNull().WithMessage("ProductId null ola bilməz.");
    }
}
