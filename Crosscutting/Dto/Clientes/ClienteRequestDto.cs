using System.ComponentModel.DataAnnotations;
using Crosscutting.Enums;

namespace Crosscutting.Dto.Clientes;

public class ClienteRequestDto
{
    [Required] public string Nome { get; set; }

    [Required] public string Cpf { get; set; }

    [Required] public DateTime DataNascimento { get; set; }

    [Required] public EstadoCivil EstadoCivil { get; set; }
}