namespace DaPraIr.API.Models;

public class Relato
{
    public int Id { get; set; }
    public int LugarId { get; set; }
    public bool DeuCerto { get; set; }
    public string? Comentario { get; set; }
    public string? NecessidadesPerfil { get; set; } // JSON: ["cadeira_de_rodas", "autismo"]
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    public Lugar Lugar { get; set; } = null!;
}
