using E_Ticaret.Application.DTOs.OrderDtos;
using FluentValidation;

namespace E_Ticaret.Application.Validations.OrderValidations;

public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
{
    public OrderUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Sifariş ID boş ola bilməz.")
            .NotNull().WithMessage("Sifariş ID tələb olunur.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId boş ola bilməz.")
            .NotNull().WithMessage("UserId tələb olunur.");
    }
}
