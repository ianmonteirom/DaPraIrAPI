namespace DaPraIr.API.DTOs;

public record BuscaPerfilDto(
    string? Bairro,
    bool UsaCadeira,
    bool SensibilidadeBarulho,
    bool NecessitaAutismo,
    string? Tipo
);
