using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface IContaService
{
<<<<<<< Updated upstream
    Task<ContaResponseDto> CadastrarConta(Guid clienteId, ContaRequestDto contaRequestDto);
    Task<ContaResponseDto> AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto, Guid id);
    Task<bool> ExcluirConta(Guid clienteId, Guid id);
    Task<ContaResponseDto> ConsultarConta(Guid clienteId, Guid id);
    Task<IEnumerable<ContaResponseDto>> ListarContas(Guid clienteId);
=======
    ContaResponseDto CadastrarConta(Guid clienteId, ContaRequestDto contaRequestDto);
    ContaResponseDto AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto, Guid id);
    bool ExcluirConta(Guid clienteId, Guid id);
    ContaResponseDto ConsultarConta(Guid clienteId, Guid id);
    IEnumerable<ContaResponseDto> ListarContas(Guid clienteId);
>>>>>>> Stashed changes
}