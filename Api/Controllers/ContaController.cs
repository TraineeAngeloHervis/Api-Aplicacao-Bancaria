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
    public async Task<IActionResult> CadastrarConta([FromBody] ContaRequestDto contaRequestDto)
    {
        if (!_contaValidator.EhValido(contaRequestDto, out var errors))
        {
            return BadRequest(errors);
        }

        var cliente = await _clienteService.ConsultarCliente(contaRequestDto.ClienteId);
        if (cliente == null)
        {
            return NotFound($"Cliente com ID {contaRequestDto.ClienteId} não encontrado.");
        }
        
        var contaCadastrada = await _contaService.CadastrarConta(contaRequestDto, contaRequestDto.ClienteId);
        return CreatedAtAction(nameof(ConsultarConta), new { id = contaCadastrada.Id }, contaCadastrada);
    }

    [HttpPut("atualizar/{id:guid}")]
    public async Task<IActionResult> AtualizarConta(Guid id, [FromBody] ContaRequestDto contaRequestDto)
    {
        var cliente = await _clienteService.ConsultarCliente(contaRequestDto.ClienteId);
        if (cliente == null)
        {
            return NotFound($"Cliente com ID {contaRequestDto.ClienteId} não encontrado.");
        }
        
        if (!_contaValidator.EhValido(contaRequestDto, out var errors))
        {
            return BadRequest(errors);
        }
        
        var contaAtualizada = await _contaService.AtualizarConta(contaRequestDto.ClienteId, contaRequestDto, id);
        return contaAtualizada == null ? NotFound() : Ok(contaAtualizada);
    }

    [HttpDelete("excluir/{id:guid}")]
    public async Task<IActionResult> ExcluirConta(Guid id)
    {
        var contaExcluida = await _contaService.ExcluirConta(id);
        return contaExcluida ? NoContent() : NotFound();
    }

    [HttpGet("consultar/{id:guid}")]
    public async Task<IActionResult> ConsultarConta(Guid id)
    {
        var conta = await _contaService.ConsultarConta(id);
        return conta == null ? NotFound() : Ok(conta);
    }

    [HttpGet("listar/{clienteId:guid}")]
    public async Task<IActionResult> ListarContas(Guid clienteId)
    {
        var contas = await _contaService.ListarContas(clienteId);
        return Ok(contas);
    }
}
