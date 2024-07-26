using Crosscutting.Dto;
using Crosscutting.Enums;
using Domain.Interfaces;
using Domain.Interfaces.Transacoes;
using FluentValidation;

namespace Domain.Validators;

public class TransferenciaValidator : AbstractValidator<TransferenciaRequestDto>, ITransferenciaValidator
{
    public TransferenciaValidator()
    {
        RuleFor(t => t.ContaOrigemId)
            .NotEmpty()
            .WithMessage("O campo ContaOrigemId é obrigatório.");

        RuleFor(t => t.ContaDestinoId)
            .NotEmpty()
            .WithMessage("O campo ContaDestinoId é obrigatório.");

        RuleFor(t => t.Valor)
            .GreaterThan(0)
            .WithMessage("O valor da transferência deve ser positivo.");
        
    }

    public Task<bool> EhValido(TransferenciaRequestDto transferenciaRequestDto, out IList<string> errors)
    {
        var validationResult = Validate(transferenciaRequestDto);
        errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        return Task.FromResult(validationResult.IsValid);
    }
}