using Bogus;
using Crosscutting.Dto;

namespace Test.Builders;

public static class ContaResponseDtoBuilder
{

    private static readonly Faker _faker = new Faker();

    public static ContaRequestDto BuildContaRequestDto()
    {
        return new ContaRequestDto
        {
            
        };
    }
}