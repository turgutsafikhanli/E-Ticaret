﻿using E_Ticaret.Application.DTOs.FileUploadDtos;
using FluentValidation;

namespace E_Ticaret.Application.Validations.FileUploadValidations;

public class FileUploadValidator : AbstractValidator<FileUploadDto>
{

    public FileUploadValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("Fayl boş ola bilməz.")
            .NotEmpty().WithMessage("Fayl seçilməlidir.")
            .Must(file => file.Length > 0).WithMessage("Faylın ölçüsü sıfırdan böyük olmalıdır.");

    }
}
