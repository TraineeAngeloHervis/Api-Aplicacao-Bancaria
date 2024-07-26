using Bogus;
using Crosscutting.Dto;
using Crosscutting.Enums;

namespace Test.Crosscutting;

public class TransacaoResponseDtoBuilder
{
    private readonly Faker<TransacaoResponseDto> _faker;
    
    private TransacaoResponseDtoBuilder()
    {
        _faker = new Faker<TransacaoResponseDto>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.ContaDestinoId, f => f.Random.Guid())
            .RuleFor(x => x.ContaOrigemId, f => f.Random.Guid())
            .RuleFor(x => x.Valor, f => f.Random.Decimal())
            .RuleFor(x => x.DataTransacao, f => f.Date.Past())
            .RuleFor(x => x.TipoTransacao, f => f.PickRandom<TipoTransacao>());
    }
    
    public static TransacaoResponseDtoBuilder Novo()
        => new();

    public TransacaoResponseDtoBuilder ComTransacaoRequestDto(TransferenciaRequestDto transferenciaRequestDto)
    {
        _faker.RuleFor(x => x.ContaDestinoId, f => transferenciaRequestDto.ContaDestinoId);
        _faker.RuleFor(x => x.ContaOrigemId, f => transferenciaRequestDto.ContaOrigemId);
        _faker.RuleFor(x => x.Valor, f => transferenciaRequestDto.Valor);
        _faker.RuleFor(x => x.TipoTransacao, f => transferenciaRequestDto.TipoTransacao);
        return this;
    }

    public TransacaoResponseDtoBuilder ComTransacaoResponseDto(TransacaoResponseDto transacaoResponseDto)
    {
        _faker.RuleFor(x => x.Id, f => transacaoResponseDto.Id);
        _faker.RuleFor(x => x.ContaDestinoId, f => transacaoResponseDto.ContaDestinoId);
        _faker.RuleFor(x => x.ContaOrigemId, f => transacaoResponseDto.ContaOrigemId);
        _faker.RuleFor(x => x.Valor, f => transacaoResponseDto.Valor);
        _faker.RuleFor(x => x.DataTransacao, f => transacaoResponseDto.DataTransacao);
        _faker.RuleFor(x => x.TipoTransacao, f => transacaoResponseDto.TipoTransacao);
        return this;
    }
    
    public TransacaoResponseDtoBuilder ComId(Guid id)
    {
        _faker.RuleFor(x => x.Id, f => id);
        return this;
    }
    
    public TransacaoResponseDtoBuilder ComContaDestinoId(Guid contaDestinoId)
    {
        _faker.RuleFor(x => x.ContaDestinoId, f => contaDestinoId);
        return this;
    }
    
    public TransacaoResponseDtoBuilder ComContaOrigemId(Guid contaOrigemId)
    {
        _faker.RuleFor(x => x.ContaOrigemId, f => contaOrigemId);
        return this;
    }
    
    public TransacaoResponseDtoBuilder ComValor(decimal valor)
    {
        _faker.RuleFor(x => x.Valor, f => valor);
        return this;
    }
    
    public TransacaoResponseDtoBuilder ComTipoTransacao(TipoTransacao tipoTransacao)
    {
        _faker.RuleFor(x => x.TipoTransacao, f => tipoTransacao);
        return this;
    }
    
    public TransacaoResponseDtoBuilder ComDataTransacao(DateTime dataTransacao)
    {
        _faker.RuleFor(x => x.DataTransacao, f => dataTransacao);
        return this;
    }
    
    public TransacaoResponseDto Build()
        => _faker.Generate();
}