using Crosscutting.Dto;
using Domain.Interfaces.Contas;
using FluentValidation;

namespace Domain.Validators;

public class ContaValidator : AbstractValidator<ContaRequestDto>, IContaValidator
{
    public ContaValidator()
    {
        RuleFor(conta => conta.ClienteId)
            .NotEmpty()
            .WithMessage("O campo ClienteId é obrigatório.");

        RuleFor(conta => conta.Saldo)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O saldo inicial não pode ser negativo.");

        RuleFor(conta => conta.DataAbertura)
            .LessThanOrEqualTo(DateTime.Now.AddSeconds(1))
            .WithMessage("A data de abertura da conta não pode ser maior que a data atual.");

        RuleFor(conta => conta.TipoConta)
            .IsInEnum()
            .WithMessage("Tipo de conta inválido.");
    }

    public Task<bool> EhValido(ContaRequestDto contaRequestDto, out IList<string> errors)
    {
        var validationResult = Validate(contaRequestDto);
        errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        return Task.FromResult(validationResult.IsValid);
    }
}