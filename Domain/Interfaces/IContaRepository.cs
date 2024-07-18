using Domain.Entities;

namespace Domain.Interfaces;

public interface IContaRepository
{
    //usando async e await para métodos assíncronos
    Task<Conta> CadastrarConta(Guid clienteId, Conta conta);
    Task<Conta> AtualizarConta(Guid clienteId, Conta conta);
    Task<bool> ExcluirConta(Guid id);
    Task<Conta> ConsultarConta(Guid id);
    Task<IEnumerable<Conta>> ListarContas(Guid clienteId);
}