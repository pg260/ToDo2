using FluentValidation;
using ToDo2.Domain.Entities;

namespace ToDo2.Domain.Validators;

public class TasksValidator : AbstractValidator<Tasks>
{
    public TasksValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("Escolha um nome para a sua task.")
            .Length(3, 25)
            .WithMessage("O nome não pode conter menos de 3 ou mais de 25 caracteres.");

        RuleFor(c => c.Descricao)
            .MaximumLength(200)
            .WithMessage("A descrição não pode ter mais de 200 caracteres.");

        RuleFor(c => c.Expiracao)
            .Must((model, expiracao) => expiracao.Date >= DateTime.Today)
            .WithMessage("A data de expiração da task não pode ser menor que a data de hoje.");
    }
}