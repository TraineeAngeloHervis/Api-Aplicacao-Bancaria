using Crosscutting.Dto;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Validators;

public class ClienteValidator : AbstractValidator<ClienteRequestDto>, IClienteValidator
{
    public ClienteValidator()
    {
        RuleFor(cliente => cliente.Nome)
            .NotEmpty()
            .WithMessage("O nome do cliente é obrigatório.");

        RuleFor(cliente => cliente.Cpf)
            .NotEmpty()
            .Matches(@"\d{11}")
            .WithMessage("O CPF do cliente é inválido.");

        RuleFor(cliente => cliente.DataNascimento)
            .LessThan(DateTime.Now)
            .WithMessage("A data de nascimento não pode ser maior que a data atual.");

        RuleFor(cliente => cliente.EstadoCivil)
            .IsInEnum()
            .WithMessage("Estado civil inválido.");
    }
    
    public bool EhValido(ClienteRequestDto clienteRequestDto, out IList<string> errors)
    {
        var result = Validate(clienteRequestDto);
        errors = result.Errors.Select(e => e.ErrorMessage).ToList();
        return result.IsValid;
    }
}