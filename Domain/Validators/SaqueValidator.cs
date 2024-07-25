using Crosscutting.Dto;
using Crosscutting.Enums;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Validators;

public class SaqueValidator : AbstractValidator<TransacaoRequestDto>, ISaqueValidator
{
    public SaqueValidator()
    {
        RuleFor(t => t.ContaOrigemId)
            .NotEmpty()
            .WithMessage("O campo ContaOrigemId é obrigatório.");

        RuleFor(t => t.Valor)
            .GreaterThan(0)
            .WithMessage("O valor do saque deve ser positivo.");

        RuleFor(t => t.TipoTransacao)
            .Equal(TipoTransacao.Saque)
            .WithMessage("Tipo de transação inválido para saque.");

        RuleFor(t => t.ContaOrigemId)
            .Equal(t => t.ContaDestinoId)
            .WithMessage("Conta de origem e conta de destino devem ser iguais.");
    }


    public bool EhValido(TransacaoRequestDto transacaoRequestDto, out IList<string> errors)
    {
        var validationResult = Validate(transacaoRequestDto);
        errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
        return validationResult.IsValid;
    }
}