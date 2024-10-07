using Bogus;
using Bogus.Extensions.Brazil;
using Crosscutting.Enums;
using Domain.Entities;

namespace Test.Crosscutting.Clientes;

public class ClienteBuilder
{
    private readonly Faker<Cliente> _faker;

    private ClienteBuilder()
    {
        _faker = new Faker<Cliente>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Nome, f => f.Person.FullName)
            .RuleFor(x => x.Cpf, f => f.Person.Cpf().Replace(".", "").Replace("-", ""))
            .RuleFor(x => x.DataNascimento, f => f.Date.Past())
            .RuleFor(x => x.EstadoCivil, f => f.PickRandom<EstadoCivil>());
    }
    
    public static ClienteBuilder Novo()
        => new();
    
    public ClienteBuilder ComCliente (Cliente cliente)
    {
        _faker.RuleFor(x => x.Id, f => cliente.Id);
        _faker.RuleFor(x => x.Nome, f => cliente.Nome);
        _faker.RuleFor(x => x.Cpf, f => cliente.Cpf);
        _faker.RuleFor(x => x.DataNascimento, f => cliente.DataNascimento);
        _faker.RuleFor(x => x.EstadoCivil, f => cliente.EstadoCivil);
        return this;
    }
    
    public Cliente Build()
        => _faker.Generate();
    
}