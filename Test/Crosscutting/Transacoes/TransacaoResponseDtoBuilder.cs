using Bogus;
using Crosscutting.Dto;

namespace Test.Crosscutting.Transacoes;

public class TransacaoResponseDtoBuilder
{
    private readonly Faker<TransacaoResponseDto> _faker;

    public TransacaoResponseDtoBuilder()
    {
        _faker = new Faker<TransacaoResponseDto>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Valor, f => f.Random.Decimal(0, 100))
            .RuleFor(x => x.ContaOrigemId, f => f.Random.Guid())
            .RuleFor(x => x.ContaDestinoId, f => f.Random.Guid())
            .RuleFor(x => x.DataTransacao, f => f.Date.Past());
    }

    public static TransacaoResponseDtoBuilder Novo()
        => new();

    public TransacaoResponseDtoBuilder ComTransacaoResponse(TransacaoResponseDto transacaoResponseDto)
    {
        _faker.RuleFor(x => x.Id, f => transacaoResponseDto.Id);
        _faker.RuleFor(x => x.Valor, f => transacaoResponseDto.Valor);
        _faker.RuleFor(x => x.ContaOrigemId, f => transacaoResponseDto.ContaOrigemId);
        _faker.RuleFor(x => x.ContaDestinoId, f => transacaoResponseDto.ContaDestinoId);
        _faker.RuleFor(x => x.DataTransacao, f => transacaoResponseDto.DataTransacao);
        return this;
    }

    public TransacaoResponseDtoBuilder ComId(Guid id)
    {
        _faker.RuleFor(x => x.Id, f => id);
        return this;
    }

    public TransacaoResponseDtoBuilder ComValor(decimal valor)
    {
        _faker.RuleFor(x => x.Valor, f => valor);
        return this;
    }

    public TransacaoResponseDtoBuilder ComContaOrigemId(Guid contaOrigemId)
    {
        _faker.RuleFor(x => x.ContaOrigemId, f => contaOrigemId);
        return this;
    }

    public TransacaoResponseDtoBuilder ComContaDestinoId(Guid contaDestinoId)
    {
        _faker.RuleFor(x => x.ContaDestinoId, f => contaDestinoId);
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