using Crosscutting.Enums;

namespace Crosscutting.Dto;

public class ClienteResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    public EstadoCivil EstadoCivil { get; set; }
}