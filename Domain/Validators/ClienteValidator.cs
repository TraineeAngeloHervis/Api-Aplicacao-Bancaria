using Crosscutting.Dto;
using Domain.Interfaces;
using FluentValidation;

namespace Domain.Validators;

public class ClienteValidator : AbstractValidator<ClienteRequestDto>, IClienteValidator
{
    public ClienteValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("O nome do cliente é obrigatório.");

        RuleFor(c => c.Cpf)
            .NotEmpty()
            .Matches(@"\d{11}")
            .WithMessage("O CPF do cliente é inválido.");

        RuleFor(c => c.DataNascimento)
            .LessThan(DateTime.Now)
            .WithMessage("A data de nascimento não pode ser maior que a data atual.");

        RuleFor(c => c.EstadoCivil)
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