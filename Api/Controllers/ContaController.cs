using Crosscutting.Dto;
using Domain.Interfaces;
using Domain.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContaController : ControllerBase
{
    private readonly IContaService _contaService;
    private readonly IClienteService _clienteService;
    private readonly ContaValidator _contaValidator;

    public ContaController(IContaService contaService, IClienteService clienteService, ContaValidator contaValidator)
    {
        _contaService = contaService;
        _clienteService = clienteService;
        _contaValidator = contaValidator;
    }

    [HttpPost("cadastrar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CadastrarConta([FromBody] ContaRequestDto contaRequestDto)
    {
        if (!_contaValidator.EhValido(contaRequestDto, out var errors)) return BadRequest(errors);
        try
        {
            var cliente = await _clienteService.ConsultarCliente(contaRequestDto.ClienteId);
            if (cliente == null) return NotFound($"Cliente com ID {contaRequestDto.ClienteId} não encontrado.");

            var contaCadastrada = await _contaService
                .CadastrarConta(contaRequestDto, contaRequestDto.ClienteId);

            return CreatedAtAction(nameof(ConsultarConta), new { id = contaCadastrada.Id }, contaCadastrada);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("atualizar/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarConta(Guid id, [FromBody] ContaRequestDto contaRequestDto)
    {
        if (!_contaValidator.EhValido(contaRequestDto, out var errors)) return BadRequest(errors);
        try
        {
            var cliente = await _clienteService.ConsultarCliente(contaRequestDto.ClienteId);
            if (cliente == null) return NotFound($"Cliente com ID {contaRequestDto.ClienteId} não encontrado.");

            var contaAtualizada = await _contaService
                .AtualizarConta(contaRequestDto.ClienteId, contaRequestDto, id);

            return Ok(contaAtualizada);
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
    public async Task<IActionResult> ExcluirConta(Guid id)
    {
        try
        {
            var conta = await _contaService.ConsultarConta(id);
            if (conta == null) return NotFound();

            var contaExcluida = await _contaService.ExcluirConta(id);
            return contaExcluida ? NoContent() : NotFound();
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
    public async Task<IActionResult> ConsultarConta(Guid id)
    {
        try
        {
            var conta = await _contaService.ConsultarConta(id);
            return conta == null ? NotFound() : Ok(conta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("listar/{clienteId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarContas(Guid clienteId)
    {
        try
        {
            var contas = await _contaService.ListarContas(clienteId);
            return Ok(contas);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}