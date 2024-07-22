using Bogus;
using Bogus.Extensions.Brazil;
using Crosscutting.Dto;
using Crosscutting.Enums;

namespace Test.Crosscutting;

public class ClienteRequestDtoBuilder
{
    private readonly Faker<ClienteRequestDto> _faker;

    private ClienteRequestDtoBuilder()
    {
        _faker = new Faker<ClienteRequestDto>("pt_BR")
            .RuleFor(x => x.Nome, f => f.Person.FullName)
            .RuleFor(x => x.Cpf, f => f.Person.Cpf().Replace(".", "").Replace("-", ""))
            .RuleFor(x => x.DataNascimento, f => f.Person.DateOfBirth)
            .RuleFor(x => x.EstadoCivil, f => f.PickRandom<EstadoCivil>());
    }

    public static ClienteRequestDtoBuilder Novo()
        => new();
    
    public ClienteRequestDtoBuilder ComClienteRequest(ClienteRequestDto clienteRequestDto)
    {
        _faker.RuleFor(x => x.Nome, f => clienteRequestDto.Nome);
        _faker.RuleFor(x => x.Cpf, f => clienteRequestDto.Cpf);
        _faker.RuleFor(x => x.DataNascimento, f => clienteRequestDto.DataNascimento);
        _faker.RuleFor(x => x.EstadoCivil, f => clienteRequestDto.EstadoCivil);
        return this;
    }

    public ClienteRequestDtoBuilder ComNome(string nome)
    {
        _faker.RuleFor(x => x.Nome, f => nome);
        return this;
    }

    public ClienteRequestDtoBuilder ComCpf(string cpf)
    {
        _faker.RuleFor(x => x.Cpf, f => cpf);
        return this;
    }

    public ClienteRequestDtoBuilder ComDataNascimento(DateTime dataNascimento)
    {
        _faker.RuleFor(x => x.DataNascimento, f => dataNascimento);
        return this;
    }

    public ClienteRequestDtoBuilder ComEstadoCivil(EstadoCivil estadoCivil)
    {
        _faker.RuleFor(x => x.EstadoCivil, f => estadoCivil);
        return this;
    }

    public ClienteRequestDto Build()
        => _faker.Generate();
}