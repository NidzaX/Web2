using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Application.Users.Commands
{
    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty();

            RuleFor(c => c.LastName).NotEmpty();

            RuleFor(c => c.Username).NotEmpty();

            RuleFor(c => c.Password)
                           .NotEmpty()
                           .MinimumLength(4)
                           .WithMessage("Invalid password.");

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email address.");

            RuleFor(c => c.Address).NotEmpty();

            RuleFor(c => c.Birthday).NotEmpty();

            RuleFor(c => c.File).NotEmpty();

        }

    }
}
