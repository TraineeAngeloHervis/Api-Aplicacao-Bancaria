using Microsoft.AspNetCore.Mvc;
using Crosscutting.Dto.Contas;
using Domain.Contas.Interfaces;

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

    [HttpPost]
    public IActionResult CadastrarConta([FromBody] ContaRequestDto contaRequestDto)
    {
        var contaCadastrada = _contaService.CadastrarConta(contaRequestDto);
        return CreatedAtAction(nameof(ConsultarConta), new { id = contaCadastrada.Id }, contaCadastrada);
    }

    [HttpPut]
    public IActionResult AtualizarConta([FromBody] ContaRequestDto contaRequestDto)
    {
        var contaAtualizada = _contaService.AtualizarConta(contaRequestDto);
        return Ok(contaAtualizada);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult ExcluirConta(Guid id)
    {
        var contaExcluida = _contaService.ExcluirConta(id);
        return contaExcluida ? NoContent() : NotFound();
    }

    [HttpGet("{id:guid}")]
    public IActionResult ConsultarConta(Guid id)
    {
        var conta = _contaService.ConsultarConta(id);
        return conta == null ? NotFound() : Ok(conta);
    }

    [HttpGet]
    public IActionResult ListarContas()
    {
        var contas = _contaService.ListarContas();
        return Ok(contas);
    }
}