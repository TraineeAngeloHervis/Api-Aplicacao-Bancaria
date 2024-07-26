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
        
        CreateMap<TransferenciaRequestDto, Transferencia>();
        CreateMap<Transferencia, TransacaoResponseDto>();
        
        CreateMap<DepositoRequestDto, Deposito>();
        CreateMap<Deposito, TransacaoResponseDto>();
        
        CreateMap<SaqueRequestDto, Saque>();
        CreateMap<Saque, TransacaoResponseDto>();
    }
}