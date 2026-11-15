using DaPraIr.API.DTOs;
using DaPraIr.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaPraIr.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LugaresController : ControllerBase
{
    private readonly ILugarService _service;

    public LugaresController(ILugarService service) => _service = service;

    /// <summary>Busca lugares compatíveis com o perfil de acessibilidade informado</summary>
    /// <remarks>
    /// Exemplo: GET /api/lugares?bairro=Pinheiros&amp;necessitaAutismo=true
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LugarResumoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Buscar([FromQuery] BuscaPerfilDto perfil)
    {
        var resultado = await _service.BuscarPorPerfilAsync(perfil);
        return Ok(resultado);
    }

    /// <summary>Retorna ficha completa do lugar, incluindo avisos ativos e histórico de relatos</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LugarDetalheDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Detalhe(int id)
    {
        var lugar = await _service.ObterDetalheAsync(id);
        return lugar is null ? NotFound(new { erro = "Lugar não encontrado." }) : Ok(lugar);
    }
}
