using Crosscutting.Dto.Contas;
using FluentValidation;

namespace Domain.Validators;

public class ContaValidator : AbstractValidator<ContaRequestDto>
{

    public ContaValidator()
    {
        RuleFor(conta => conta.ClienteId)
            .NotEmpty()
            .WithMessage("O campo ClienteId é obrigatório");
        
        RuleFor(conta => conta.SaldoInicial)
            .GreaterThanOrEqualTo(0).WithMessage("O saldo inicial não pode ser negativo");

        RuleFor(conta => conta.DataAbertura)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de abertura não pode ser maior que a data atual");
        
        RuleFor(conta => conta.TipoConta.ToLower())
            .Matches(@"(corrente|poupança|poupanca)").WithMessage("Tipo de conta inválido");
    }
}