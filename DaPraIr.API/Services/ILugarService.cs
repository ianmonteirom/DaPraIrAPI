using DaPraIr.API.DTOs;
using DaPraIr.API.Models;

namespace DaPraIr.API.Services;

public interface ILugarService
{
    Task<IEnumerable<LugarResumoDto>> BuscarPorPerfilAsync(BuscaPerfilDto perfil);
    Task<LugarDetalheDto?> ObterDetalheAsync(int id);
    Task<Relato> RegistrarRelatoAsync(int lugarId, RelataVisitaDto dto);
    Task<Aviso> CriarAvisoAsync(int lugarId, CriarAvisoDto dto);
    Task<IEnumerable<AvisoDto>> ObterAvisosAtivosAsync(int lugarId);
}
