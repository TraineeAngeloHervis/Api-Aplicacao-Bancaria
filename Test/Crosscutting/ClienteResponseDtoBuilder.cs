using Bogus;
using Bogus.Extensions.Brazil;
using Crosscutting.Dto;
using Crosscutting.Enums;

namespace Test.Crosscutting;

public class ClienteResponseDtoBuilder
{
    private readonly Faker<ClienteResponseDto> _faker;

    private ClienteResponseDtoBuilder()
    {
        _faker = new Faker<ClienteResponseDto>("pt_BR")
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Nome, f => f.Person.FullName)
            .RuleFor(x => x.Cpf, f => f.Person.Cpf())
            .RuleFor(x => x.DataNascimento, f => f.Person.DateOfBirth)
            .RuleFor(x => x.EstadoCivil, f => f.PickRandom<EstadoCivil>());
    }

    public static ClienteResponseDtoBuilder Novo()
        => new();

    public ClienteResponseDtoBuilder ComId(Guid id)
    {
        _faker.RuleFor(x => x.Id, f => id);
        return this;
    }

    public ClienteResponseDtoBuilder ComNome(string nome)
    {
        _faker.RuleFor(x => x.Nome, f => nome);
        return this;
    }

    public ClienteResponseDtoBuilder ComCpf(string cpf)
    {
        _faker.RuleFor(x => x.Cpf, f => cpf);
        return this;
    }

    public ClienteResponseDtoBuilder ComDataNascimento(DateTime dataNascimento)
    {
        _faker.RuleFor(x => x.DataNascimento, f => dataNascimento);
        return this;
    }

    public ClienteResponseDtoBuilder ComEstadoCivil(EstadoCivil estadoCivil)
    {
        _faker.RuleFor(x => x.EstadoCivil, f => estadoCivil);
        return this;
    }

    public ClienteResponseDto Build()
        => _faker.Generate();
}