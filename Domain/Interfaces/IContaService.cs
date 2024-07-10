using Crosscutting.Dto;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IContaService
{
    ContaResponseDto CadastrarConta(Guid clienteId, ContaRequestDto contaRequestDto);
    ContaResponseDto AtualizarConta(Guid clienteId, ContaRequestDto contaRequestDto, Guid id);
    bool ExcluirConta(Guid clienteId, Guid id);
    ContaResponseDto ConsultarConta(Guid clienteId, Guid id);
    IEnumerable<ContaResponseDto> ListarContas(Guid clienteId);
}