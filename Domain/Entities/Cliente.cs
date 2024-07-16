using Crosscutting.Enums;

<<<<<<< Updated upstream
namespace Domain.Entities;

public class Cliente
{
    public Guid Id { get; init; }
=======
<<<<<<<< Updated upstream:Crosscutting/Dto/ClienteResponseDto.cs
namespace Crosscutting.Dto;
========
namespace Domain.Entities;
>>>>>>>> Stashed changes:Domain/Entities/Cliente.cs

public class ClienteResponseDto
{
    public Guid Id { get; set; }
>>>>>>> Stashed changes
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    public EstadoCivil EstadoCivil { get; set; }
    public ICollection<Conta> Contas { get; set; }
}