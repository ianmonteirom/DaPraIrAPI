namespace DaPraIr.API.Models;

public class Ficha
{
    public int Id { get; set; }
    public int LugarId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string PassoAPasso { get; set; } = string.Empty;
    public string NivelBarulho { get; set; } = "médio"; // baixo, médio, alto
    public string HorariosTranquilos { get; set; } = string.Empty;
    public bool TemElevador { get; set; }
    public bool TemRampa { get; set; }
    public bool TemBanheiroAdaptado { get; set; }
    public bool AtendePCD { get; set; }
    public bool AtendeAutismo { get; set; }
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
    public DateTime ValidadeAte { get; set; } = DateTime.UtcNow.AddMonths(3);

    public Lugar Lugar { get; set; } = null!;
}
