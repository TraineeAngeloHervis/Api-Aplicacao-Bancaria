using Bogus;
using Crosscutting.Dto;

namespace Test.Crosscutting.Transacoes;

public class TransferenciaRequestDtoBuilder
{
    private readonly Faker<TransferenciaRequestDto> _faker;
    
    public TransferenciaRequestDtoBuilder()
    {
        _faker = new Faker<TransferenciaRequestDto>("pt_BR")
            .RuleFor(x => x.Valor, f => f.Random.Decimal(0, 100))
            .RuleFor(x => x.ContaOrigemId, f => f.Random.Guid())
            .RuleFor(x => x.ContaDestinoId, f => f.Random.Guid());
    }
    
    public static TransferenciaRequestDtoBuilder Novo()
        => new();

    public TransferenciaRequestDtoBuilder ComTransferenciaRequest(TransferenciaRequestDto transferenciaRequestDto)
    {
        _faker.RuleFor(x => x.Valor, f => transferenciaRequestDto.Valor);
        _faker.RuleFor(x => x.ContaOrigemId, f => transferenciaRequestDto.ContaOrigemId);
        _faker.RuleFor(x => x.ContaDestinoId, f => transferenciaRequestDto.ContaDestinoId);
        return this;
    }
    
    public TransferenciaRequestDtoBuilder ComValor(decimal valor)
    {
        _faker.RuleFor(x => x.Valor, f => valor);
        return this;
    }
    
    public TransferenciaRequestDtoBuilder ComContaOrigemId(Guid contaOrigemId)
    {
        _faker.RuleFor(x => x.ContaOrigemId, f => contaOrigemId);
        return this;
    }
    
    public TransferenciaRequestDtoBuilder ComContaDestinoId(Guid contaDestinoId)
    {
        _faker.RuleFor(x => x.ContaDestinoId, f => contaDestinoId);
        return this;
    }
    
    public TransferenciaRequestDto Build()
        => _faker.Generate();
    
}