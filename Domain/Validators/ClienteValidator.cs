using Crosscutting.Dto.Clientes;
using FluentValidation;

namespace Domain.Validators;

public class ClienteValidator : AbstractValidator<ClienteRequestDto>
{
    public ClienteValidator()
    {
        RuleFor(cliente => cliente.Nome)
            .NotEmpty().WithMessage("O nome do cliente é obrigatório.");

        RuleFor(cliente => cliente.Cpf)
            .NotEmpty().WithMessage("O CPF do cliente é obrigatório.")
            .Matches(@"\d{11}").WithMessage("Digite apenas números.");
        
        RuleFor(cliente => cliente.DataNascimento)
            .LessThan(DateTime.Now).WithMessage("A data de nascimento não pode ser maior que a data atual.");
        
        RuleFor(cliente => cliente.EstadoCivil.ToLower())
            .NotEmpty().WithMessage("O estado civil do cliente é obrigatório.")
            .Matches(@"(solteiro|casado|divorciado|viúvo|viuvo)").WithMessage("Estado civil inválido.");
    }
}