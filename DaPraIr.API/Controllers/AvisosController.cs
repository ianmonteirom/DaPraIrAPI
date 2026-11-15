using DaPraIr.API.DTOs;
using DaPraIr.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaPraIr.API.Controllers;

[ApiController]
[Route("api/lugares/{lugarId:int}/avisos")]
[Produces("application/json")]
public class AvisosController : ControllerBase
{
    private readonly ILugarService _service;

    public AvisosController(ILugarService service) => _service = service;

    /// <summary>Lista avisos ativos do lugar (elevador quebrado, obra, horário especial)</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AvisoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(int lugarId)
    {
        var avisos = await _service.ObterAvisosAtivosAsync(lugarId);
        return Ok(avisos);
    }

    /// <summary>Registra novo aviso para o lugar — qualquer pessoa pode avisar a comunidade</summary>
    /// <remarks>
    /// Exemplo de body: { "titulo": "Elevador quebrado", "descricao": "Acesso B fora de serviço", "tipo": "elevador", "expiraEm": "2026-11-22T23:59:00Z" }
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar(int lugarId, [FromBody] CriarAvisoDto dto)
    {
        var aviso = await _service.CriarAvisoAsync(lugarId, dto);
        return CreatedAtAction(nameof(Listar), new { lugarId }, aviso);
    }
}
