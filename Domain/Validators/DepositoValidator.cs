using Crosscutting.Dto;
using Domain.Interfaces.Transacoes;
using FluentValidation;

namespace Domain.Validators;

public class DepositoValidator : AbstractValidator<DepositoRequestDto>, IDepositoValidator
{
    public DepositoValidator()
    {
        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("O valor do depósito deve ser maior que zero.");
        
        RuleFor(x => x.ContaOrigemId)
            .NotEmpty()
            .WithMessage("O campo ContaOrigemId é obrigatório.");
    }   
    
    public Task<bool> EhValido(DepositoRequestDto depositoRequestDto, out IList<string> errors)
    {
        var validationResult = Validate(depositoRequestDto);
        errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        return Task.FromResult(validationResult.IsValid);
    }
}