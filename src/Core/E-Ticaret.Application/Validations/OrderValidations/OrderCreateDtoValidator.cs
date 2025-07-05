using E_Ticaret.Application.DTOs.OrderDtos;
using FluentValidation;

namespace E_Ticaret.Application.Validations.OrderValidations;

public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId boş ola bilməz.")
            .NotNull().WithMessage("UserId tələb olunur.");
    }
}
