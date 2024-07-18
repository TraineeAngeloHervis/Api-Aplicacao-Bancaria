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

    public ClienteController(IClienteService clienteService, IClienteValidator clienteValidator)
    {
        _clienteService = clienteService;
        _clienteValidator = clienteValidator;
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> CadastrarCliente([FromBody] ClienteRequestDto clienteRequestDto)
    {
        if (!_clienteValidator.EhValido(clienteRequestDto, out var errors))
        {
            return BadRequest(errors);
        }

        var clienteCadastrado = await _clienteService.CadastrarCliente(clienteRequestDto);
        return CreatedAtAction(nameof(ConsultarCliente), new { id = clienteCadastrado.Id }, clienteCadastrado);
    }

    [HttpPut("atualizar/{id:guid}")]
    public async Task<IActionResult> AtualizarCliente(Guid id, [FromBody] ClienteRequestDto clienteRequestDto)
    {
        if (!_clienteValidator.EhValido(clienteRequestDto, out var errors))
        {
            return BadRequest(errors);
        }

        var clienteAtualizado = await _clienteService.AtualizarCliente(id, clienteRequestDto);
        return clienteAtualizado == null ? NotFound() : Ok(clienteAtualizado);
    }

    [HttpDelete("excluir/{id:guid}")]
    public async Task<IActionResult> ExcluirCliente(Guid id)
    {
        var clienteExcluido = await _clienteService.ExcluirCliente(id);
        return clienteExcluido ? NoContent() : NotFound();
    }

    [HttpGet("consultar/{id:guid}")]
    public async Task<IActionResult> ConsultarCliente(Guid id)
    {
        var cliente = await _clienteService.ConsultarCliente(id);
        return cliente == null ? NotFound() : Ok(cliente);
    }

    [HttpGet("listar")]
    public async Task<IActionResult> ListarClientes()
    {
        var clientes = await _clienteService.ListarClientes();
        return Ok(clientes);
    }
}