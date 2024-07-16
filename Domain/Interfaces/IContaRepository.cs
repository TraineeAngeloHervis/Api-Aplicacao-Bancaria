using Domain.Entities;

namespace Domain.Interfaces;

public interface IContaRepository
{
    Conta CadastrarConta(Guid clienteId, Conta conta);
    Conta AtualizarConta(Guid clienteId, Conta conta);
    bool ExcluirConta(Guid clienteId, Guid id);
    Conta ConsultarConta(Guid clienteId, Guid id);
    IEnumerable<Conta> ListarContas(Guid clienteId);
}