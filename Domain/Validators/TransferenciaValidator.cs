using Crosscutting.Dto;
using Crosscutting.Enums;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Validators
{
    public class TransferenciaValidator : AbstractValidator<TransacaoRequestDto>, ITransferenciaValidator
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
            
            RuleFor(t => t.TipoTransacao)
                .Equal(TipoTransacao.Transferencia)
                .WithMessage("Tipo de transação inválido para transferência.");
        }

        public bool EhValido(TransacaoRequestDto transacaoRequestDto, out IList<string> errors)
        {
            var result = Validate(transacaoRequestDto);
            errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            return result.IsValid;
        }
    }
}