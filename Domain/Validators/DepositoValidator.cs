using Crosscutting.Dto;
using Crosscutting.Enums;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Validators;

public class DepositoValidator : AbstractValidator<TransacaoRequestDto>, IDepositoValidator
{
    public DepositoValidator()
    {
        RuleFor(t => t.ContaOrigemId)
            .NotEmpty()
            .WithMessage("O campo ContaOrigemId é obrigatório.");
                
        RuleFor(t => t.Valor)
            .GreaterThan(0)
            .WithMessage("O valor do depósito deve ser positivo.");
            
        RuleFor(t => t.TipoTransacao)
            .Equal(TipoTransacao.Deposito)
            .WithMessage("Tipo de transação inválido para depósito.");
        
        RuleFor(t => t.ContaOrigemId)
            .Equal(t => t.ContaDestinoId)
            .WithMessage("Conta de origem e conta de destino devem ser iguais.");
    }   

    public bool EhValido(TransacaoRequestDto transacaoRequestDto, out IList<string> errors)
    {
        var result = Validate(transacaoRequestDto);
        errors = result.Errors.Select(e => e.ErrorMessage).ToList();
        return result.IsValid;
    }
}