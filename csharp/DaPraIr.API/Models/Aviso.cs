namespace DaPraIr.API.Models;

public class Aviso
{
    public int Id { get; set; }
    public int LugarId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty; // elevador, horario, obra, acessibilidade
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiraEm { get; set; }

    public Lugar Lugar { get; set; } = null!;
}
