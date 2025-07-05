using E_Ticaret.Application.DTOs.FavouriteDtos;
using FluentValidation;

namespace E_Ticaret.Application.Validations.FavouriteValidations;

public class FavouriteCreateDtoValidator : AbstractValidator<FavouriteCreateDto>
{
    public FavouriteCreateDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Ad Id cannot be empty.")
            .NotNull().WithMessage("Ad Id cannot be null.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .NotNull().WithMessage("Name cannot be null.");
    }
}
