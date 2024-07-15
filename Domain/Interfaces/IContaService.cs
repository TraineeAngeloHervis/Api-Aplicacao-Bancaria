using Crosscutting.Dto;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IContaService
{
    Task<ContaResponseDto> CadastrarConta(Guid clienteId, ContaRequestDto contaRequestDto);
    Task<ContaResponseDto> AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto, Guid id);
    Task<bool> ExcluirConta(Guid clienteId, Guid id);
    Task<ContaResponseDto> ConsultarConta(Guid clienteId, Guid id);
    Task<IEnumerable<ContaResponseDto>> ListarContas(Guid clienteId);
}