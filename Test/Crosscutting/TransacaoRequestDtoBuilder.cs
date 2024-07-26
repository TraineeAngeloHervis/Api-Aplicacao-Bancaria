using Bogus;
using Crosscutting.Dto;
using Crosscutting.Enums;

namespace Test.Crosscutting;

public class TransacaoRequestDtoBuilder
{
    private readonly Faker<TransferenciaRequestDto> _faker;
    
    private TransacaoRequestDtoBuilder()
    {
        _faker = new Faker<TransferenciaRequestDto>("pt_BR")
            .RuleFor(x => x.ContaOrigemId, f => f.Random.Guid())
            .RuleFor(x => x.Valor, f => f.Random.Decimal())
            .RuleFor(x => x.TipoTransacao, f => f.PickRandom<TipoTransacao>());
    }
    
    public static TransacaoRequestDtoBuilder Novo()
        => new();

    public TransacaoRequestDtoBuilder ComTransacaoRequestDto(TransferenciaRequestDto transferenciaRequestDto)
    {
        _faker.RuleFor(x => x.ContaDestinoId, f => transferenciaRequestDto.ContaDestinoId);
        _faker.RuleFor(x => x.ContaOrigemId, f => transferenciaRequestDto.ContaOrigemId);
        _faker.RuleFor(x => x.Valor, f => transferenciaRequestDto.Valor);
        _faker.RuleFor(x => x.TipoTransacao, f => transferenciaRequestDto.TipoTransacao);
        return this;
    }
    
    
    public TransacaoRequestDtoBuilder ComContaOrigemId(Guid contaOrigemId)
    {
        _faker.RuleFor(x => x.ContaOrigemId, f => contaOrigemId);
        return this;
    }
    
    public TransacaoRequestDtoBuilder ComValor(decimal valor)
    {
        _faker.RuleFor(x => x.Valor, f => valor);
        return this;
    }
    
    public TransacaoRequestDtoBuilder ComTipoTransacao(TipoTransacao tipoTransacao)
    {
        _faker.RuleFor(x => x.TipoTransacao, f => tipoTransacao);
        return this;
    }
    
    public TransferenciaRequestDto Build()
        => _faker.Generate();
}