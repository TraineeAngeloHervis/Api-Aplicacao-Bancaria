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
<<<<<<< Updated upstream
    public async Task<IActionResult> CadastrarConta(Guid clienteId, [FromBody] ContaRequestDto contaRequestDto)
    {
        var contaCadastrada = await _contaService.CadastrarConta(clienteId, contaRequestDto);
=======
    public IActionResult CadastrarConta(Guid clienteId, [FromBody] ContaRequestDto contaRequestDto)
    {
        var contaCadastrada = _contaService.CadastrarConta(clienteId, contaRequestDto);
>>>>>>> Stashed changes
        return CreatedAtAction(nameof(ConsultarConta), new { clienteId, id = contaCadastrada.Id }, contaCadastrada);
    }
    
    [HttpPut("atualizar/{clienteId:guid}/{id:guid}")]
<<<<<<< Updated upstream
    public async Task<IActionResult> AtualizarConta(Guid clienteId, Guid id, [FromBody] ContaRequestDto contaRequestDto)
    {
        var contaAtualizada = await _contaService.AtualizarConta(clienteId, contaRequestDto, id);
=======
    public IActionResult AtualizarConta(Guid clienteId, Guid id, [FromBody] ContaRequestDto contaRequestDto)
    {
        var contaAtualizada = _contaService.AtualizarConta(clienteId, contaRequestDto, id);
>>>>>>> Stashed changes
        return contaAtualizada == null ? NotFound() : Ok(contaAtualizada);
    }
    
    [HttpDelete("excluir/{clienteId:guid}/{id:guid}")]
<<<<<<< Updated upstream
    public async Task<IActionResult> ExcluirConta(Guid clienteId, Guid id)
    {
        var contaExcluida = await _contaService.ExcluirConta(clienteId, id);
=======
    public IActionResult ExcluirConta(Guid clienteId, Guid id)
    {
        var contaExcluida = _contaService.ExcluirConta(clienteId, id);
>>>>>>> Stashed changes
        return contaExcluida ? NoContent() : NotFound();
    }
    
    [HttpGet("consultar/{clienteId:guid}/{id:guid}")]
<<<<<<< Updated upstream
    public async Task<IActionResult> ConsultarConta(Guid clienteId, Guid id)
    {
        var conta = await _contaService.ConsultarConta(clienteId, id);
=======
    public IActionResult ConsultarConta(Guid clienteId, Guid id)
    {
        var conta = _contaService.ConsultarConta(clienteId, id);
>>>>>>> Stashed changes
        return conta == null ? NotFound() : Ok(conta);
    }
    
    [HttpGet("listar/{clienteId:guid}")]
<<<<<<< Updated upstream
    public async Task<IActionResult> ListarContas(Guid clienteId)
    {
        var contas = await _contaService.ListarContas(clienteId);
        return contas == null ? NotFound() : Ok(contas);
=======
    public IActionResult ListarContas(Guid clienteId)
    {
        var contas = _contaService.ListarContas(clienteId);
        return Ok(contas);
>>>>>>> Stashed changes
    }
}