using Crosscutting.Dto;
using Domain.Interfaces.Clientes;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastrarCliente([FromBody] ClienteRequestDto clienteRequestDto)
    {
        if (!await _clienteValidator.EhValido(clienteRequestDto, out var errors)) return BadRequest(errors);

        try
        {
            var clienteCadastrado = await _clienteService.CadastrarCliente(clienteRequestDto);
            return CreatedAtAction(nameof(ConsultarCliente), new { id = clienteCadastrado.Id }, clienteCadastrado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("atualizar/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AtualizarCliente(Guid id, [FromBody] ClienteRequestDto clienteRequestDto)
    {
        if (!await _clienteValidator.EhValido(clienteRequestDto, out var errors)) return BadRequest(errors);

        try
        {
            var clienteAtualizado = await _clienteService.AtualizarCliente(id, clienteRequestDto);
            return Ok(clienteAtualizado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("excluir/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExcluirCliente(Guid id)
    {
        try
        {
            var clienteExcluido = await _clienteService.ExcluirCliente(id);
            return clienteExcluido ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("consultar/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarCliente(Guid id)
    {
        try
        {
            var cliente = await _clienteService.ConsultarCliente(id);
            return Ok(cliente);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("listar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarClientes()
    {
        try
        {
            var clientes = await _clienteService.ListarClientes();
            return Ok(clientes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}