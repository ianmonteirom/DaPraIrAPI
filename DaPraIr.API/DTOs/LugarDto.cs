namespace DaPraIr.API.DTOs;

public record LugarResumoDto(
    int Id,
    string Nome,
    string Tipo,
    string Bairro,
    string Endereco,
    double TaxaSucesso,
    int TotalRelatos,
    int AvisosAtivos,
    bool DaPraIr,
    string Recomendacao
);

public record LugarDetalheDto(
    int Id,
    string Nome,
    string Tipo,
    string Bairro,
    string Endereco,
    string Cidade,
    double? Latitude,
    double? Longitude,
    FichaDto? Ficha,
    IEnumerable<AvisoDto> AvisosAtivos,
    double TaxaSucesso,
    int TotalRelatos
);

public record FichaDto(
    string Descricao,
    string PassoAPasso,
    string NivelBarulho,
    string HorariosTranquilos,
    bool TemElevador,
    bool TemRampa,
    bool TemBanheiroAdaptado,
    bool AtendePCD,
    bool AtendeAutismo,
    DateTime ValidadeAte
);
