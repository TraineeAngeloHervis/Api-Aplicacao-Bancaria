using AutoMapper;
using Crosscutting.Dto.Clientes;
using Crosscutting.Dto.Contas;
using Domain.Clientes.Entities;
using Domain.Contas.Entities;

namespace Api.AutoMapper;

public static class AutoMapperConfiguracao
{
    public static void AddConfiguracaoAutoMapper(IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
    
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClienteRequestDto, Cliente>();
            CreateMap<Cliente, ClienteResponseDto>();

            CreateMap<ContaRequestDto, Conta>();
            CreateMap<Conta, ContaResponseDto>();
        }
    }
}