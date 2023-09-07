
using FluentValidation.Results;

namespace ToDo2.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    
    public virtual bool Validar(out ValidationResult validationResult)
    {
        validationResult = new ValidationResult();
        return validationResult.IsValid;
    }
}