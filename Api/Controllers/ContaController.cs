using Crosscutting.Dto;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContaController : ControllerBase
{
    private readonly IContaService _contaService;

    public ContaController(IContaService contaService)
    {
        _contaService = contaService;
    }
    
    [HttpPost("cadastrar/{clienteId:guid}")]
    public IActionResult CadastrarConta(Guid clienteId, [FromBody] ContaRequestDto contaRequestDto)
    {
        var contaCadastrada = _contaService.CadastrarConta(clienteId, contaRequestDto);
        return CreatedAtAction(nameof(ConsultarConta), new { clienteId, id = contaCadastrada.Id }, contaCadastrada);
    }
    
    [HttpPut("atualizar/{clienteId:guid}/{id:guid}")]
    public IActionResult AtualizarConta(Guid clienteId, Guid id, [FromBody] ContaRequestDto contaRequestDto)
    {
        var contaAtualizada = _contaService.AtualizarConta(clienteId, contaRequestDto, id);
        return contaAtualizada == null ? NotFound() : Ok(contaAtualizada);
    }
    
    [HttpDelete("excluir/{clienteId:guid}/{id:guid}")]
    public IActionResult ExcluirConta(Guid clienteId, Guid id)
    {
        var contaExcluida = _contaService.ExcluirConta(clienteId, id);
        return contaExcluida ? NoContent() : NotFound();
    }
    
    [HttpGet("consultar/{clienteId:guid}/{id:guid}")]
    public IActionResult ConsultarConta(Guid clienteId, Guid id)
    {
        var conta = _contaService.ConsultarConta(clienteId, id);
        return conta == null ? NotFound() : Ok(conta);
    }
    
    [HttpGet("listar/{clienteId:guid}")]
    public IActionResult ListarContas(Guid clienteId)
    {
        var contas = _contaService.ListarContas(clienteId);
        return Ok(contas);
    }
}