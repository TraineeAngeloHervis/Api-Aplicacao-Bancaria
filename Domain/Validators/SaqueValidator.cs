using Crosscutting.Dto;
using Domain.Interfaces.Transacoes;
using FluentValidation;

namespace Domain.Validators;

public class SaqueValidator : AbstractValidator<SaqueRequestDto>, ISaqueValidator
{
    public SaqueValidator()
    {
        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("O valor do saque deve ser maior que zero.");
        
        RuleFor(x => x.ContaOrigemId)
            .NotEmpty()
            .WithMessage("O campo ContaOrigemId é obrigatório.");
    }

    public Task<bool> EhValido(SaqueRequestDto saqueRequestDto, out IList<string> errors)
    {
        var validationResult = Validate(saqueRequestDto);
        errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        return Task.FromResult(validationResult.IsValid);
    }
}