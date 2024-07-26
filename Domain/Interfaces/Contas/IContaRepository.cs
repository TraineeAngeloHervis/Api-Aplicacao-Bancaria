using Domain.Entities;

namespace Domain.Interfaces.Contas;

public interface IContaRepository
{
    Task<Conta> CadastrarConta(Guid clienteId, Entities.Conta conta);
    Task<Conta> AtualizarConta(Guid clienteId, Entities.Conta conta, Guid id);
    Task<bool> ExcluirConta(Guid id);
    Task<Entities.Conta> ConsultarConta(Guid id);
    Task<IEnumerable<Entities.Conta>> ListarContas(Guid clienteId);
}