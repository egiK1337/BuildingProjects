using FluentValidation;
using ServiceLayer.Abstractions.DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Validations.AdminValidations
{
    public class AdminValidator : AbstractValidator<AdminDto>
    {
        public AdminValidator()
        {
            RuleFor(adminDto => adminDto.Name)
            .NotEmpty();
            RuleFor(adminDto => adminDto.Password)
            .NotEmpty();
        }
    }
}
