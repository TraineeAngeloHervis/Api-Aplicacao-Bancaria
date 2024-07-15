using Crosscutting.Dto;
using FluentValidation;

namespace Crosscutting.Validators;

public class ClienteValidator : AbstractValidator<ClienteRequestDto>
{
    public ClienteValidator()
    {
        RuleFor(cliente => cliente.Nome)
            .NotEmpty()
            .WithMessage("O nome do cliente é obrigatório.");

        RuleFor(cliente => cliente.Cpf)
            .NotEmpty()
            .WithMessage("O CPF do cliente é obrigatório.")
            .Matches(@"\d{11}")
            .WithMessage("O CPF do cliente é inválido.");

        RuleFor(cliente => cliente.DataNascimento)
            .LessThan(DateTime.Now)
            .WithMessage("A data de nascimento não pode ser maior que a data atual.");

        RuleFor(cliente => cliente.EstadoCivil)
            .IsInEnum()
            .WithMessage("Estado civil inválido.");
    }
}