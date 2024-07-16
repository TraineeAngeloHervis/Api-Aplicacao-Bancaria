using Crosscutting.Enums;

<<<<<<<< Updated upstream:Crosscutting/Dto/ClienteResponseDto.cs
namespace Crosscutting.Dto;
========
namespace Domain.Entities;
>>>>>>>> Stashed changes:Domain/Entities/Cliente.cs

public class ClienteResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    public EstadoCivil EstadoCivil { get; set; }
    public ICollection<Conta> Contas { get; set; }
}