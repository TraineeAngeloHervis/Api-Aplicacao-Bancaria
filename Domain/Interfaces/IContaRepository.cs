using Domain.Entities;

namespace Domain.Interfaces;

public interface IContaRepository
{
<<<<<<< Updated upstream
    Task<Conta> CadastrarConta(Guid clienteId, Conta conta);
    Task<Conta> AtualizarConta(Guid clienteId, Conta conta);
    Task<bool> ExcluirConta(Guid clienteId, Guid id);
    Task<Conta> ConsultarConta(Guid clienteId, Guid id);
    Task<IEnumerable<Conta>> ListarContas(Guid clienteId);
=======
    //a conta precisa passar pelo cliente
    Conta CadastrarConta(Guid clienteId, Conta conta);
    Conta AtualizarConta(Guid clienteId, Conta conta);
    bool ExcluirConta(Guid clienteId, Guid id);
    Conta ConsultarConta(Guid clienteId, Guid id);
    IEnumerable<Conta> ListarContas(Guid clienteId);
>>>>>>> Stashed changes
}