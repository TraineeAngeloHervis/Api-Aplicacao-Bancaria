using Bogus;
using Crosscutting.Dto;
using Crosscutting.Enums;
using Domain.Entities;
using Test.Crosscutting.Contas;

namespace Test.Crosscutting.Transacoes;

public class TransacaoBuilder
{
    private readonly Faker<Transacao> _faker;

    private TransacaoBuilder()
    {
        _faker = new Faker<Transacao>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.ContaOrigemId, f => f.Random.Guid())
            .RuleFor(x => x.ContaOrigem, f => ContaBuilder.Novo().Build())
            .RuleFor(x => x.ContaDestinoId, f => f.Random.Guid())
            .RuleFor(x => x.ContaDestino, f => ContaBuilder.Novo().Build())
            .RuleFor(x => x.Valor, f => f.Random.Decimal(0, 100))
            .RuleFor(x => x.DataTransacao, f => f.Date.Past());
    }
    
    public static TransacaoBuilder Novo()
        => new();

    public TransacaoBuilder ComTransacao(Transacao transacao)
    {
        _faker.RuleFor(x => x.Id, f => transacao.Id);
        _faker.RuleFor(x => x.ContaOrigemId, f => transacao.ContaOrigemId);
        _faker.RuleFor(x => x.ContaDestinoId, f => transacao.ContaDestinoId);
        _faker.RuleFor(x => x.Valor, f => transacao.Valor);
        _faker.RuleFor(x => x.DataTransacao, f => transacao.DataTransacao);
        return this;
    }
    
    public TransacaoBuilder ComDepositoRequest(DepositoRequestDto depositoRequestDto)
    {
        _faker.RuleFor(x => x.ContaOrigemId, f => depositoRequestDto.ContaOrigemId);
        _faker.RuleFor(x => x.Valor, f => depositoRequestDto.Valor);
        _faker.RuleFor(x => x.TipoTransacao, f => TipoTransacao.Deposito);
        return this;
    }
    
    public TransacaoBuilder ComSaqueRequest(SaqueRequestDto saqueRequestDto)
    {
        _faker.RuleFor(x => x.ContaOrigemId, f => saqueRequestDto.ContaOrigemId);
        _faker.RuleFor(x => x.Valor, f => saqueRequestDto.Valor);
        _faker.RuleFor(x => x.TipoTransacao, f => TipoTransacao.Saque);
        return this;
    }
    
    public TransacaoBuilder ComTransferenciaRequest(TransferenciaRequestDto transferenciaRequestDto)
    {
        _faker.RuleFor(x => x.ContaOrigemId, f => transferenciaRequestDto.ContaOrigemId);
        _faker.RuleFor(x => x.ContaDestinoId, f => transferenciaRequestDto.ContaDestinoId);
        _faker.RuleFor(x => x.Valor, f => transferenciaRequestDto.Valor);
        return this;
    }
    
    public Transacao Build()
        => _faker.Generate();
}