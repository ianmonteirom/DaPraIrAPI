using System.Text.Json;
using DaPraIr.API.Data;
using DaPraIr.API.DTOs;
using DaPraIr.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DaPraIr.API.Services;

public class LugarService : ILugarService
{
    private readonly AppDbContext _context;

    public LugarService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<LugarResumoDto>> BuscarPorPerfilAsync(BuscaPerfilDto perfil)
    {
        var query = _context.Lugares
            .Include(l => l.Ficha)
            .Include(l => l.Avisos)
            .Include(l => l.Relatos)
            .AsQueryable();

        if (!string.IsNullOrEmpty(perfil.Bairro))
            query = query.Where(l => l.Bairro.ToLower().Contains(perfil.Bairro.ToLower()));

        if (!string.IsNullOrEmpty(perfil.Tipo))
            query = query.Where(l => l.Tipo.ToLower() == perfil.Tipo.ToLower());

        if (perfil.UsaCadeira)
            query = query.Where(l => l.Ficha != null && l.Ficha.TemRampa);

        if (perfil.SensibilidadeBarulho)
            query = query.Where(l => l.Ficha != null && l.Ficha.NivelBarulho == "baixo");

        if (perfil.NecessitaAutismo)
            query = query.Where(l => l.Ficha != null && l.Ficha.AtendeAutismo);

        var lugares = await query.ToListAsync();

        return lugares
            .Select(l => MapToResumo(l, perfil))
            .OrderByDescending(l => l.TaxaSucesso);
    }

    public async Task<LugarDetalheDto?> ObterDetalheAsync(int id)
    {
        var lugar = await _context.Lugares
            .Include(l => l.Ficha)
            .Include(l => l.Avisos)
            .Include(l => l.Relatos)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (lugar is null) return null;

        var avisosAtivos = lugar.Avisos
            .Where(a => a.Ativo && (a.ExpiraEm == null || a.ExpiraEm > DateTime.UtcNow))
            .Select(a => new AvisoDto(a.Id, a.Titulo, a.Descricao, a.Tipo, a.CriadoEm, a.ExpiraEm))
            .ToList();

        var taxa = CalcularTaxa(lugar.Relatos);

        FichaDto? fichaDto = lugar.Ficha is null ? null : new FichaDto(
            lugar.Ficha.Descricao, lugar.Ficha.PassoAPasso, lugar.Ficha.NivelBarulho,
            lugar.Ficha.HorariosTranquilos, lugar.Ficha.TemElevador, lugar.Ficha.TemRampa,
            lugar.Ficha.TemBanheiroAdaptado, lugar.Ficha.AtendePCD, lugar.Ficha.AtendeAutismo,
            lugar.Ficha.ValidadeAte);

        return new LugarDetalheDto(
            lugar.Id, lugar.Nome, lugar.Tipo, lugar.Bairro, lugar.Endereco, lugar.Cidade,
            lugar.Latitude, lugar.Longitude, fichaDto, avisosAtivos, taxa, lugar.Relatos.Count);
    }

    public async Task<Relato> RegistrarRelatoAsync(int lugarId, RelataVisitaDto dto)
    {
        var relato = new Relato
        {
            LugarId = lugarId,
            DeuCerto = dto.DeuCerto,
            Comentario = dto.Comentario,
            NecessidadesPerfil = dto.NecessidadesPerfil is not null
                ? JsonSerializer.Serialize(dto.NecessidadesPerfil)
                : null,
            CriadoEm = DateTime.UtcNow
        };

        _context.Relatos.Add(relato);
        await _context.SaveChangesAsync();
        return relato;
    }

    public async Task<Aviso> CriarAvisoAsync(int lugarId, CriarAvisoDto dto)
    {
        var aviso = new Aviso
        {
            LugarId = lugarId,
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Tipo = dto.Tipo,
            Ativo = true,
            CriadoEm = DateTime.UtcNow,
            ExpiraEm = dto.ExpiraEm
        };

        _context.Avisos.Add(aviso);
        await _context.SaveChangesAsync();
        return aviso;
    }

    public async Task<IEnumerable<AvisoDto>> ObterAvisosAtivosAsync(int lugarId) =>
        await _context.Avisos
            .Where(a => a.LugarId == lugarId && a.Ativo &&
                        (a.ExpiraEm == null || a.ExpiraEm > DateTime.UtcNow))
            .Select(a => new AvisoDto(a.Id, a.Titulo, a.Descricao, a.Tipo, a.CriadoEm, a.ExpiraEm))
            .ToListAsync();

    // ── helpers ──────────────────────────────────────────────────────────────

    private static LugarResumoDto MapToResumo(Lugar l, BuscaPerfilDto perfil)
    {
        var avisosAtivos = l.Avisos
            .Where(a => a.Ativo && (a.ExpiraEm == null || a.ExpiraEm > DateTime.UtcNow))
            .ToList();

        var taxa = CalcularTaxa(l.Relatos);
        var bloqueiosCadeira = perfil.UsaCadeira && avisosAtivos.Any(a => a.Tipo == "elevador");
        var daPraIr = taxa >= 60 && !bloqueiosCadeira;

        return new LugarResumoDto(
            l.Id, l.Nome, l.Tipo, l.Bairro, l.Endereco,
            taxa, l.Relatos.Count, avisosAtivos.Count,
            daPraIr,
            GerarRecomendacao(l, avisosAtivos, taxa, perfil));
    }

    private static double CalcularTaxa(ICollection<Relato> relatos)
    {
        if (!relatos.Any()) return 0;
        return Math.Round((double)relatos.Count(r => r.DeuCerto) / relatos.Count * 100, 1);
    }

    private static string GerarRecomendacao(Lugar l, List<Aviso> avisos, double taxa, BuscaPerfilDto perfil)
    {
        var partes = new List<string>();

        partes.Add(taxa >= 70 ? "Dá pra ir!" : taxa >= 40 ? "Pode funcionar." : "Pode ser difícil.");

        if (!string.IsNullOrEmpty(l.Ficha?.HorariosTranquilos))
            partes.Add($"Melhor horário: {l.Ficha.HorariosTranquilos}.");

        foreach (var aviso in avisos.Take(2))
            partes.Add($"Aviso: {aviso.Titulo}.");

        return string.Join(" ", partes);
    }
}
