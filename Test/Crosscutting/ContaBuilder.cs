﻿using Bogus;
using Crosscutting.Enums;
using Domain.Entities;

namespace Test.Crosscutting;

public class ContaBuilder
{
    private readonly Faker<Conta> _faker;

    private ContaBuilder()
    {
        _faker = new Faker<Conta>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.ClienteId, f => f.Random.Guid())
            .RuleFor(x => x.Cliente, f => ClienteBuilder.Novo().Build())
            .RuleFor(x => x.SaldoInicial, f => f.Random.Decimal())
            .RuleFor(x => x.TipoConta, f => f.PickRandom<TipoConta>())
            .RuleFor(x => x.DataAbertura, f => f.Date.Past());
    }
    
    public static ContaBuilder Novo()
        => new();
    
    public ContaBuilder ComConta(Conta conta)
    {
        _faker.RuleFor(x => x.Id, f => conta.Id);
        _faker.RuleFor(x => x.ClienteId, f => conta.ClienteId);
        _faker.RuleFor(x => x.SaldoInicial, f => conta.SaldoInicial);
        _faker.RuleFor(x => x.TipoConta, f => conta.TipoConta);
        _faker.RuleFor(x => x.DataAbertura, f => conta.DataAbertura);
        return this;
    }

    public Conta Build()
        => _faker.Generate();
}