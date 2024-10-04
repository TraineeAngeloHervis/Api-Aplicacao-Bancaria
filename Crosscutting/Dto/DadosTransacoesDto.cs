namespace Crosscutting.Dto;

public class DadosContaDto
{
    public Guid ContaId { get; set; }
    public DateTime DataAbertura { get; set; }
    public List<DadosTransacaoDto> Transacoes { get; set; }
}

public class DadosTransacaoDto
{
    public Guid TransacaoId { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataTransacao { get; set; }
}