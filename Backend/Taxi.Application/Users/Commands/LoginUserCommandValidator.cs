using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Application.Users.Commands
{
    internal class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            //check why this validation isn't working
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email address.");

            RuleFor(c => c.Password)
                .NotEmpty()
                .MinimumLength(4)
                .WithMessage("Invalid password.");
        }
    }
}
