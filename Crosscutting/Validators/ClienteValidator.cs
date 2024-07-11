using Crosscutting.Dto;
using Crosscutting.Exceptions;
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
            .Matches(@"\d{11}").WithMessage("O CPF do cliente é inválido.");

        RuleFor(cliente => cliente.DataNascimento)
            .LessThan(DateTime.Now)
            .WithMessage("A data de nascimento não pode ser maior que a data atual.");

        RuleFor(cliente => cliente.EstadoCivil)
            .NotEmpty()
            .WithMessage("O estado civil do cliente é obrigatório.")
            .IsInEnum()
            .WithMessage("Estado civil inválido.");
    }
    
    public void ValidarCliente(ClienteRequestDto clienteRequestDto)
    {
        var validationResult = Validate(clienteRequestDto);
        if (!validationResult.IsValid)
        {
            var firstError = validationResult.Errors[0];
            throw new ClienteInvalidoException(firstError.ErrorMessage);
        }
    }
}