using Crosscutting.Dto.Contas;
using Domain.Clientes.Interfaces;
using Domain.Clientes.Entities;
using Crosscutting.Dto.Clientes;
using Infra.Data;
using AutoMapper;

namespace Domain.Clientes.Services;

public class ClienteService : IClienteService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    
    public ClienteService(AppDbContext _context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}