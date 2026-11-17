namespace DaPraIr.API.DTOs;

public record AvisoDto(
    int Id,
    string Titulo,
    string Descricao,
    string Tipo,
    DateTime CriadoEm,
    DateTime? ExpiraEm
);

public record CriarAvisoDto(
    string Titulo,
    string Descricao,
    string Tipo,
    DateTime? ExpiraEm
);
