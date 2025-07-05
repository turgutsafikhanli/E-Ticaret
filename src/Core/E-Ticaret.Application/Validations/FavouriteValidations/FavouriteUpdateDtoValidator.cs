using E_Ticaret.Application.DTOs.FavouriteDtos;
using FluentValidation;

namespace E_Ticaret.Application.Validations.FavouriteValidations;

public class FavouriteUpdateDtoValidator : AbstractValidator<FavouriteUpdateDto>
{
    public FavouriteUpdateDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Favorite ID cannot be empty.")
            .NotNull().WithMessage("Favorite ID cannot be null.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("User ID cannot be empty.")
            .NotNull().WithMessage("User ID cannot be null.");
    }
}
