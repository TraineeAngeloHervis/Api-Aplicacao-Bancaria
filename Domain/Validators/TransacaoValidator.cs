using Crosscutting.Dto;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Validators;

public class TransacaoValidator : AbstractValidator<TransacaoRequestDto>, ITransacaoValidator
{
    public TransacaoValidator()
    {
        RuleFor(t => t.ContaOrigemId)
            .NotEmpty()
            .WithMessage("O campo ContaOrigemId é obrigatório.");
        
        RuleFor(t => t.ContaDestinoId)
            .NotEmpty()
            .WithMessage("O campo ContaDestinoId é obrigatório.");
        
        RuleFor(t => t.Valor)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O valor da transação não pode ser negativo.");
        
        RuleFor(t => t.TipoTransacao)
            .IsInEnum()
            .WithMessage("Tipo de transação inválido.");
    }
    
    public bool EhValido(TransacaoRequestDto transacaoRequestDto, out IList<string> errors)
    {
        var result = Validate(transacaoRequestDto);
        errors = result.Errors.Select(e => e.ErrorMessage).ToList();
        return result.IsValid;
    }
}