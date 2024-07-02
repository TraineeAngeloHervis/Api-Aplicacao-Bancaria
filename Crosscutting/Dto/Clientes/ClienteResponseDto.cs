namespace Crosscutting.Dto.Clientes;

public class ClienteResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    public string EstadoCivil { get; set; }

    public static ClienteResponseDto ComCpfInvalido()
        => new()
        {
            Id = Guid.Empty,
            Nome = string.Empty,
            Cpf = "CPF inválido",
            DataNascimento = DateTime.MinValue,
            EstadoCivil = string.Empty
        };
    
    public static ClienteResponseDto ComNomeInvalido()
        => new()
        {
            Id = Guid.Empty,
            Nome = "Nome inválido",
            Cpf = string.Empty,
            DataNascimento = DateTime.MinValue,
            EstadoCivil = string.Empty
        };
    
    public static ClienteResponseDto ComDataNascimentoInvalida()
        => new()
        {
            Id = Guid.Empty,
            Nome = string.Empty,
            Cpf = string.Empty,
            DataNascimento = DateTime.MinValue,
            EstadoCivil = string.Empty
        };
    
    public static ClienteResponseDto ComEstadoCivilInvalido()
        => new()
        {
            Id = Guid.Empty,
            Nome = string.Empty,
            Cpf = string.Empty,
            DataNascimento = DateTime.MinValue,
            EstadoCivil = "Estado civil inválido"
        };
}