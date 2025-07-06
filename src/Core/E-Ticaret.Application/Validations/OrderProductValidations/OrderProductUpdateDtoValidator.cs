using E_Ticaret.Application.DTOs.OrderProductDtos;
using FluentValidation;

namespace E_Ticaret.Application.Validations.OrderProductValidations;

public class OrderProductUpdateDtoValidator : AbstractValidator<OrderProductUpdateDto>
{
    public OrderProductUpdateDtoValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Qiymət 0-dan böyük olmalıdır.")
            .LessThanOrEqualTo(10000).WithMessage("Qiymət 10000-dən çox olmamalıdır.");
    }
}
