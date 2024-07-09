using Crosscutting.Dto.Clientes;
using Domain.Clientes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpPost("cadastrar")]
    public IActionResult CadastrarCliente([FromBody] ClienteRequestDto clienteRequestDto)
    {
        var clienteCadastrado = _clienteService.CadastrarCliente(clienteRequestDto);
        return CreatedAtAction(nameof(ConsultarCliente), new { id = clienteCadastrado.Id }, clienteCadastrado);
    }

    [HttpPut("atualizar")]
    public IActionResult AtualizarCliente([FromBody] ClienteRequestDto clienteRequestDto)
    {
        var clienteAtualizado = _clienteService.AtualizarCliente(clienteRequestDto);
        return Ok(clienteAtualizado);
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