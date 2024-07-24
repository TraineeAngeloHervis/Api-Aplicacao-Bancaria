using AutoMapper;
using Crosscutting.Dto;
using Domain.Entities;

namespace Api.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ClienteRequestDto, Cliente>();
        CreateMap<Cliente, ClienteResponseDto>();

        CreateMap<ContaRequestDto, Conta>();
        CreateMap<Conta, ContaResponseDto>();
        
        CreateMap<TransacaoRequestDto, Transacao>();
        CreateMap<Transacao, TransacaoResponseDto>();
    }
}