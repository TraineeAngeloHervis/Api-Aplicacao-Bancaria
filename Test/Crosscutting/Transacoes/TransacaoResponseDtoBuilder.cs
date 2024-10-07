using Bogus;
using Crosscutting.Dto;
using Crosscutting.Enums;
using Domain.Entities;

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
            .RuleFor(x => x.DataTransacao, f => f.Date.Past());
    }

    public static TransacaoResponseDtoBuilder Novo()
        => new();


    public TransacaoResponseDtoBuilder ComTransacao(Transacao transacao )
    {
        _faker.RuleFor(x => x.Id, f => transacao.Id);
        _faker.RuleFor(x => x.Valor, f => transacao.Valor);
        _faker.RuleFor(x => x.ContaOrigemId, f => transacao.ContaOrigemId);
        _faker.RuleFor(x => x.ContaDestinoId, f => transacao.ContaDestinoId);
        _faker.RuleFor(x => x.DataTransacao, f => transacao.DataTransacao);
        _faker.RuleFor(x => x.TipoTransacao, f => transacao.TipoTransacao);
        return this;
    }
    public TransacaoResponseDtoBuilder ComDepositoRequest(DepositoRequestDto depositoRequestDto)
    {
        _faker.RuleFor(x => x.Valor, f => depositoRequestDto.Valor);
        _faker.RuleFor(x => x.ContaOrigemId, f => depositoRequestDto.ContaOrigemId);
        _faker.RuleFor(x => x.TipoTransacao, f => TipoTransacao.Deposito);
        return this;
    }
    
    public TransacaoResponseDtoBuilder ComSaqueRequest(SaqueRequestDto saqueRequestDto)
    {
        _faker.RuleFor(x => x.Valor, f => saqueRequestDto.Valor);
        _faker.RuleFor(x => x.ContaOrigemId, f => saqueRequestDto.ContaOrigemId);
        _faker.RuleFor(x => x.TipoTransacao, f => TipoTransacao.Saque);
        return this;
    }
    
    public TransacaoResponseDtoBuilder ComTransferenciaRequest(TransferenciaRequestDto transferenciaRequestDto)
    {
        _faker.RuleFor(x => x.Valor, f => transferenciaRequestDto.Valor);
        _faker.RuleFor(x => x.ContaOrigemId, f => transferenciaRequestDto.ContaOrigemId);
        _faker.RuleFor(x => x.ContaDestinoId, f => transferenciaRequestDto.ContaDestinoId);
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