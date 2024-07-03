using AutoMapper;
using Crosscutting.Dto.Clientes;
using Crosscutting.Dto.Contas;
using Domain.Clientes.Entities;
using Domain.Contas.Entities;

namespace Api.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ClienteRequestDto, Cliente>();
        CreateMap<Cliente, ClienteResponseDto>();
        
        CreateMap<ContaResponseDto, Conta>();
        CreateMap<Conta, ContaResponseDto>();
    }
}