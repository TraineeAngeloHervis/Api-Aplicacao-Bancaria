using Crosscutting.Dto;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacaoController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;
    private readonly ITransacaoValidator _transacaoValidator;

    public TransacaoController(ITransacaoService transacaoService, ITransacaoValidator transacaoValidator)
    {
        _transacaoService = transacaoService;
        _transacaoValidator = transacaoValidator;
    }

    [HttpPost("sacar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RealizarSaque([FromBody] TransacaoRequestDto transacaoRequestDto)
    {
        if (!_transacaoValidator.EhValido(transacaoRequestDto, out var errors)) return BadRequest(errors);

        try
        {
            var transacao = await _transacaoService.RealizarSaque(transacaoRequestDto);
            return CreatedAtAction(nameof(ConsultarTransacao), new { id = transacao.Id }, transacao);
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
    public async Task<IActionResult> RealizarDeposito([FromBody] TransacaoRequestDto transacaoRequestDto)
    {
        if (!_transacaoValidator.EhValido(transacaoRequestDto, out var errors)) return BadRequest(errors);

        try
        {
            var transacao = await _transacaoService.RealizarDeposito(transacaoRequestDto);
            return CreatedAtAction(nameof(ConsultarTransacao), new { id = transacao.Id }, transacao);
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
    public async Task<IActionResult> RealizarTransferencia([FromBody] TransacaoRequestDto transacaoRequestDto)
    {
        if (!_transacaoValidator.EhValido(transacaoRequestDto, out var errors)) return BadRequest(errors);

        try
        {
            var transacao = await _transacaoService.RealizarTransferencia(transacaoRequestDto);
            return CreatedAtAction(nameof(ConsultarTransacao), new { id = transacao.Id }, transacao);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    //JSON para teste
    //{
    //    "ContaOrigemId": "b3b3b3b3-b3b3-b3b3-b3b3-b3b3b3b3b3b3",
    //    "ContaDestinoId": "b3b3b3b3-b3b3-b3b3-b3b3-b3b3b3b3b3b3",
    //    "Valor": 100,
    //    "TipoTransacao": 2
    //}

    [HttpGet("consultar/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConsultarTransacao(Guid id)
    {
        try
        {
            var transacao = await _transacaoService.ConsultarTransacao(id);
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
            var transacoes = await _transacaoService.GerarExtrato(contaId);
            return Ok(transacoes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}