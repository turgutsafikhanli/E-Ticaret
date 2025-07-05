﻿using E_Ticaret.Application.DTOs.CategoryDtos;
using FluentValidation;

namespace E_Ticaret.Application.Validations;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz.")
               .MaximumLength(50).WithMessage("Kateqoriya adı maksimum 50 simvol ola bilər.");
    }
}
