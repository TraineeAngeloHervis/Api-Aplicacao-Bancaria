using Domain.Entities;

namespace Domain.Interfaces;

public interface IContaRepository
{
    Conta CadastrarConta(Guid clienteId, Conta conta);
    Conta AtualizarConta(Guid clienteId, Conta conta);
    bool ExcluirConta(Guid id);
    Conta ConsultarConta(Guid id);
    IEnumerable<Conta> ListarContas(Guid clienteId);
}