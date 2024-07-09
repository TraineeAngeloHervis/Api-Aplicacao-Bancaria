using System.ComponentModel.DataAnnotations;

namespace Crosscutting.Dto.Contas;

public class ContaRequestDto
{
    [Required] public Guid ClienteId { get; set; }

    [Required] public decimal SaldoInicial { get; set; }

    [Required] public string TipoConta { get; set; }

    [Required] public DateTime DataAbertura { get; set; } = DateTime.Now;
}