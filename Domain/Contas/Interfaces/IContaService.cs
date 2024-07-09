using Crosscutting.Dto.Contas;

namespace Domain.Contas.Interfaces;

public interface IContaService
{
    ContaResponseDto CadastrarConta(ContaRequestDto contaRequestDto);
    ContaResponseDto AtualizarConta(ContaRequestDto contaRequestDto);
    bool ExcluirConta(Guid id);
    ContaResponseDto ConsultarConta(Guid id);
    IEnumerable<ContaResponseDto> ListarContas();
}