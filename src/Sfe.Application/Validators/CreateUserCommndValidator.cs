using FluentValidation;
using Sfe.Application.Commands;

namespace Sfe.Application.Validators
{
    public class CreateUserCommndValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommndValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("O Nome é obrigatório");
        }
    }
}
