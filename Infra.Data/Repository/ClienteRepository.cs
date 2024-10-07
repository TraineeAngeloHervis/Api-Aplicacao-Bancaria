using Crosscutting.Dto;
using Dapper;
using Domain.Entities;
using Domain.Interfaces.Clientes;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> CadastrarCliente(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> AtualizarCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<bool> ExcluirCliente(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Cliente> ConsultarCliente(Guid id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ListarClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<IEnumerable<DadosContaDto>> ConsultarTransacoes(Guid id)
        {
            // var resultado = await _context.Clientes
            //     .Where(cliente => cliente.Id == id)
            //     .Select(cliente => cliente.Contas.Select(conta => new DadosContaDto
            //     {
            //         ContaId = conta.Id,
            //         DataAbertura = conta.DataAbertura,
            //         Transacoes = conta.Transacoes.Select(transacao => new DadosTransacaoDto
            //         {
            //             Valor = transacao.Valor,
            //             DataTransacao = transacao.DataTransacao,
            //             TransacaoId = transacao.Id
            //         }).ToList()
            //     })).FirstOrDefaultAsync();
            //
            // return resultado;
            return null;
        }        
        public async Task<IEnumerable<DadosContaDto>> ConsultarTransacoesDapper(Guid id)
        {
            using var connection = AppDbContext.GetDapperContext();
            const string query = @"
                SELECT c.Id as ContaId, ct.DataAbertura,
                    t.Id as Transacoes_TransacaoId,
                    t.Valor as Transacoes_Valor,
                    t.DataTransacao as Transacoes_DataTransacao
                FROM Clientes c
                JOIN Contas ct ON c.Id = ct.ClienteId
                JOIN Transacoes t ON ct.Id = t.ContaOrigemId
                WHERE c.Id = @id";
            // Slapper.AutoMapper.Cache.ClearInstanceCache();
            // Slapper.AutoMapper.Configuration.AddIdentifier(typeof(DadosContaDto), nameof(DadosContaDto.ContaId));
            // Slapper.AutoMapper.Configuration.AddIdentifier(typeof(DadosTransacaoDto), nameof(DadosTransacaoDto.TransacaoId));
            var resultado = await connection.QueryAsync<DadosContaDto, DadosTransacaoDto, DadosContaDto>(
                query,
                (conta, transacao) =>
                {
                    return new DadosContaDto()
                    {
                        ContaId = conta.ContaId,
                        DataAbertura = conta.DataAbertura,
                        Transacoes = conta.Transacoes.ToList()
                    };
                }, new {id}, splitOn: "Transacao_TransacaoId");
            return resultado;
        }
        
        public async Task<IEnumerable<Cliente>> ListarClientesComDapper()
        {
            using var connection = AppDbContext.GetDapperContext();
            const string query = "SELECT * FROM Clientes";
            return await connection.QueryAsync<Cliente>(query);
        }
    }
}