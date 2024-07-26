using Bogus;
using Crosscutting.Enums;
using Domain.Entities;
using Test.Crosscutting.Clientes;

namespace Test.Crosscutting.Contas;

public class ContaBuilder
{
    private readonly Faker<Conta> _faker;

    private ContaBuilder()
    {
        _faker = new Faker<Conta>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.ClienteId, f => f.Random.Guid())
            .RuleFor(x => x.Cliente, f => ClienteBuilder.Novo().Build())
            .RuleFor(x => x.Saldo, f => f.Random.Decimal())
            .RuleFor(x => x.TipoConta, f => f.PickRandom<TipoConta>())
            .RuleFor(x => x.DataAbertura, f => f.Date.Past());
    }
    
    public static ContaBuilder Novo()
        => new();
    
    public ContaBuilder ComSaldo(decimal saldo)
    {
        _faker.RuleFor(x => x.Saldo, f => saldo);
        return this;
    }

    public Conta Build()
        => _faker.Generate();
}