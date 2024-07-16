using Crosscutting.Dto;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly IClienteValidator _clienteValidator;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpPost("cadastrar")]
    //usar _clienteValidator.EhValido()
    

    [HttpPut("atualizar/{id:guid}")]
    public IActionResult AtualizarCliente(Guid id, [FromBody] ClienteRequestDto clienteRequestDto)
    {
        var clienteAtualizado = _clienteService.AtualizarCliente(id, clienteRequestDto);
        return clienteAtualizado == null ? NotFound() : Ok(clienteAtualizado);
    }

    [HttpDelete("excluir/{id:guid}")]
    public IActionResult ExcluirCliente(Guid id)
    {
        var clienteExcluido = _clienteService.ExcluirCliente(id);
        return clienteExcluido ? NoContent() : NotFound();
    }

    [HttpGet("consultar/{id:guid}")]
    public IActionResult ConsultarCliente(Guid id)
    {
        var cliente = _clienteService.ConsultarCliente(id);
        return cliente == null ? NotFound() : Ok(cliente);
    }

    [HttpGet("listar")]
    public IActionResult ListarClientes()
    {
        var clientes = _clienteService.ListarClientes();
        return Ok(clientes);
    }
}