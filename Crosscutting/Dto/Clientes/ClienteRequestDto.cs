using System.ComponentModel.DataAnnotations;

namespace Crosscutting.Dto.Clientes;

public class ClienteRequestDto
{
    [Required] public string Nome { get; set; }

    [Required] public string Cpf { get; set; }

    [Required] public DateTime DataNascimento { get; set; }

    [Required] public string EstadoCivil { get; set; }
}