using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface IContaService
{
    Task<ContaResponseDto> CadastrarConta(ContaRequestDto contaRequestDto, Guid clienteId);
    Task<ContaResponseDto> AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto, Guid id);
    Task<bool> ExcluirConta(Guid id);
    Task<ContaResponseDto> ConsultarConta(Guid id);
    Task<IEnumerable<ContaResponseDto>> ListarContas(Guid clienteId);
}