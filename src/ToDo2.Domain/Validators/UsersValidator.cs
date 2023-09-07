using FluentValidation;
using ToDo2.Domain.Entities;

namespace ToDo2.Domain.Validators;

public class UsersValidator : AbstractValidator<Users>
{
    public UsersValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("Escolha um nome para o seu usuário.")
            .Length(3, 25)
            .WithMessage("O nome não pode conter menos de 3 ou mais de 25 caracteres.");
        
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Digite um email.")
            .Length(6, 80)
            .WithMessage("O email não pode conter menos de 6 ou mais de 80 caracteres.");

        RuleFor(c => c.Senha)
            .NotEmpty()
            .WithMessage("Digite uma senha.")
            .Length(4, 25)
            .WithMessage("A senha não pode conter menos de 4 ou mais de 25 caracteres.");
    }
}