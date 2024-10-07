using Crosscutting.Dto;
using Domain.Interfaces.Transacoes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacaoController(
    ISaqueService saqueService,
    IDepositoService depositoService,
    ITransferenciaService transferenciaService,
    ITransacaoService transacaoService,
    ISaqueValidator saqueValidator,
    IDepositoValidator depositoValidator,
    ITransferenciaValidator transferenciaValidator)
    : ControllerBase
{
    [HttpPost("sacar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RealizarSaque([FromBody] SaqueRequestDto saqueRequestDto)
    {
        if (!await saqueValidator.EhValido(saqueRequestDto, out var errors)) return BadRequest(errors);

        try
        {
            var saqueRealizado = await saqueService.RealizarSaque(saqueRequestDto);
            return CreatedAtAction(nameof(ConsultarTransacao), new { id = saqueRealizado.Id }, saqueRealizado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("depositar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RealizarDeposito([FromBody] DepositoRequestDto depositoRequestDto)
    {
        if (!await depositoValidator.EhValido(depositoRequestDto, out var errors)) return BadRequest(errors);

        try
        {
            var depositoRealizado = await depositoService.RealizarDeposito(depositoRequestDto);
            return CreatedAtAction(nameof(ConsultarTransacao), new { id = depositoRealizado.Id }, depositoRealizado);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("transferir")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RealizarTransferencia([FromBody] TransferenciaRequestDto transferenciaRequestDto)
    {
        if (!await transferenciaValidator.EhValido(transferenciaRequestDto, out var errors)) return BadRequest(errors);

        try
        {
            var transferenciaRealizada = await transferenciaService.RealizarTransferencia(transferenciaRequestDto);
            return CreatedAtAction(nameof(ConsultarTransacao), new { id = transferenciaRealizada.Id },
                transferenciaRealizada);
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
    public async Task<IActionResult> ConsultarTransacao(Guid id)
    {
        try
        {
            var transacao = await transacaoService.ConsultarTransacao(id);
            return transacao == null ? NotFound() : Ok(transacao);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("extrato/{contaId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarTransacoes(Guid contaId)
    {
        try
        {
            var transacoes = await transacaoService.GerarExtrato(contaId);
            return Ok(transacoes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}