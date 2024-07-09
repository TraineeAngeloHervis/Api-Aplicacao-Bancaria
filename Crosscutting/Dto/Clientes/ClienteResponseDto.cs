namespace Crosscutting.Dto.Clientes;

public class ClienteResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    public string EstadoCivil { get; set; }
}