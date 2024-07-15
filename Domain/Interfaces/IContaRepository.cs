using Domain.Entities;

namespace Domain.Interfaces;

public interface IContaRepository
{
    Task<Conta> CadastrarConta(Guid clienteId, Conta conta);
    Task<Conta> AtualizarConta(Guid clienteId, Conta conta);
    Task<bool> ExcluirConta(Guid clienteId, Guid id);
    Task<Conta> ConsultarConta(Guid clienteId, Guid id);
    Task<IEnumerable<Conta>> ListarContas(Guid clienteId);
}