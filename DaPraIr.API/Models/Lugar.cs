namespace DaPraIr.API.Models;

public class Lugar
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty; // UBS, Escola, Estacao, Dentista, Farmacia
    public string Endereco { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = "São Paulo";
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    public Ficha? Ficha { get; set; }
    public ICollection<Relato> Relatos { get; set; } = new List<Relato>();
    public ICollection<Aviso> Avisos { get; set; } = new List<Aviso>();
}
