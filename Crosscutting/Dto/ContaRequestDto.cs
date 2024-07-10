using System.ComponentModel.DataAnnotations;
using Crosscutting.Enums;

namespace Crosscutting.Dto;

public class ContaRequestDto
{
    [Required] public Guid ClienteId { get; set; }

    [Required] public decimal SaldoInicial { get; set; }

    [Required] public TipoConta TipoConta { get; set; }

    [Required] public DateTime DataAbertura { get; set; } = DateTime.Now;
}