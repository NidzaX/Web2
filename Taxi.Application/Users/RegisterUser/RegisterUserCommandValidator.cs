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
            RuleFor(c => c.firstName).NotEmpty();

            RuleFor(c => c.lastName).NotEmpty();

            RuleFor(c => c.username).NotEmpty();

            RuleFor(c => c.password)
                           .NotEmpty()
                           .MinimumLength(4)
                           .WithMessage("Invalid password.");

            RuleFor(c => c.email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email address.");

            RuleFor(c => c.address).NotEmpty();

            RuleFor(c => c.birthday).NotEmpty();

            RuleFor(c => c.file).NotEmpty();

        }

    }
}
