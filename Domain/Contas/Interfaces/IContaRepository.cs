using Domain.Contas.Entities;

namespace Domain.Contas.Interfaces;

public interface IContaRepository
{
    Conta CadastrarConta(Conta conta);
    Conta AtualizarConta(Conta conta);
    bool ExcluirConta(Guid id);
    Conta ConsultarConta(Guid id);
    IEnumerable<Conta> ListarContas();
}