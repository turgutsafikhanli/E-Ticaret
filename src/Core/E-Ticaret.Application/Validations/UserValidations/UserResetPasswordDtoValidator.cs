using E_Ticaret.Application.DTOs.UserDtos;
using FluentValidation;

namespace E_Ticaret.Application.Validations.UserValidations;

public class UserResetPasswordDtoValidator : AbstractValidator<UserResetPasswordDto>
{
    public UserResetPasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.");
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters long.")
            .MaximumLength(20).WithMessage("New password must not exceed 20 characters.");
    }
}
