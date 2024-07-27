using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Application.Users.ChangeUserPassword
{
    internal sealed class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidator()
        {
            RuleFor(c => c.Email)
                 .NotEmpty()
                 .EmailAddress()
                 .WithMessage("Invalid email address.");

            RuleFor(c => c.OldPassword)
                .NotEmpty()
                .MinimumLength(4)
                .WithMessage("Invalid password.");

            RuleFor(c => c.NewPassword)
                .NotEmpty()
                .MinimumLength(4);

        }
    }
}
