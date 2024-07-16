using Crosscutting.Dto;

namespace Domain.Interfaces;

public interface IContaService
{
    ContaResponseDto CadastrarConta(Guid clienteId, ContaRequestDto contaRequestDto);
    ContaResponseDto AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto, Guid id);
    bool ExcluirConta(Guid id);
    ContaResponseDto ConsultarConta(Guid id);
    IEnumerable<ContaResponseDto> ListarContas(Guid clienteId);
}