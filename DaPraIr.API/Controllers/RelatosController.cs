using DaPraIr.API.DTOs;
using DaPraIr.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DaPraIr.API.Controllers;

[ApiController]
[Route("api/lugares/{lugarId:int}/relatos")]
[Produces("application/json")]
public class RelatosController : ControllerBase
{
    private readonly ILugarService _service;

    public RelatosController(ILugarService service) => _service = service;

    /// <summary>Registra relato pós-visita de forma anônima — "deu certo?"</summary>
    /// <remarks>
    /// Cada relato alimenta a taxa de sucesso do lugar e pode se tornar evidência para a prefeitura.
    /// Exemplo: { "deuCerto": false, "comentario": "Elevador quebrado de novo", "necessidadesPerfil": ["cadeira_de_rodas"] }
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Relatar(int lugarId, [FromBody] RelataVisitaDto dto)
    {
        var relato = await _service.RegistrarRelatoAsync(lugarId, dto);
        return Created(
            $"/api/lugares/{lugarId}/relatos",
            new { relato.Id, relato.DeuCerto, mensagem = "Relato registrado. Obrigado por contribuir!" });
    }
}
