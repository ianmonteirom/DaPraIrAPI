namespace DaPraIr.API.DTOs;

public record RelataVisitaDto(
    bool DeuCerto,
    string? Comentario,
    string[]? NecessidadesPerfil
);
