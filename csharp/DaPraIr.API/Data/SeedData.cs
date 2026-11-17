using DaPraIr.API.Models;

namespace DaPraIr.API.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Lugares.Any()) return;

        var lugares = new List<Lugar>
        {
            new()
            {
                Nome = "UBS Vila Mariana",
                Tipo = "UBS",
                Endereco = "Rua Domingos de Morais, 1500",
                Bairro = "Vila Mariana",
                Cidade = "São Paulo",
                Latitude = -23.5932,
                Longitude = -46.6378,
                Ficha = new Ficha
                {
                    Descricao = "Unidade básica de saúde com atendimento geral",
                    PassoAPasso = "1. Entre pela rua principal. 2. Recepção à esquerda. 3. Tire a senha no painel eletrônico.",
                    NivelBarulho = "médio",
                    HorariosTranquilos = "terça e quinta, das 14h às 16h",
                    TemElevador = false,
                    TemRampa = true,
                    TemBanheiroAdaptado = true,
                    AtendePCD = true,
                    AtendeAutismo = false
                }
            },
            new()
            {
                Nome = "Estação Paraíso - Metrô",
                Tipo = "Estacao",
                Endereco = "Av. Paulista, 2000",
                Bairro = "Paraíso",
                Cidade = "São Paulo",
                Latitude = -23.5774,
                Longitude = -46.6469,
                Ficha = new Ficha
                {
                    Descricao = "Estação com elevadores e piso tátil em todos os níveis",
                    PassoAPasso = "1. Entre pelo acesso principal. 2. Use o elevador à direita da catraca. 3. Bilheteria no nível 2.",
                    NivelBarulho = "alto",
                    HorariosTranquilos = "fora do pico: 10h–16h e após 20h",
                    TemElevador = true,
                    TemRampa = true,
                    TemBanheiroAdaptado = true,
                    AtendePCD = true,
                    AtendeAutismo = false
                },
                Avisos = new List<Aviso>
                {
                    new()
                    {
                        Titulo = "Elevador do acesso B em manutenção",
                        Descricao = "Use o elevador do acesso A na Rua Vergueiro. Previsão de retorno: 3 dias.",
                        Tipo = "elevador",
                        Ativo = true,
                        ExpiraEm = DateTime.UtcNow.AddDays(3)
                    }
                }
            },
            new()
            {
                Nome = "Clínica Sorrir Para Todos",
                Tipo = "Dentista",
                Endereco = "Rua das Flores, 420",
                Bairro = "Pinheiros",
                Cidade = "São Paulo",
                Latitude = -23.5648,
                Longitude = -46.6922,
                Ficha = new Ficha
                {
                    Descricao = "Clínica especializada em atendimento odontológico para autistas e PCDs",
                    PassoAPasso = "1. Sala de espera tranquila, sem TV nem música alta. 2. Profissionais treinados em TEA. 3. Avise na recepção sobre necessidades especiais.",
                    NivelBarulho = "baixo",
                    HorariosTranquilos = "manhã, qualquer dia da semana",
                    TemElevador = false,
                    TemRampa = true,
                    TemBanheiroAdaptado = false,
                    AtendePCD = true,
                    AtendeAutismo = true
                }
            }
        };

        context.Lugares.AddRange(lugares);

        context.Relatos.AddRange(new List<Relato>
        {
            new() { LugarId = 1, DeuCerto = true, Comentario = "Atendimento ótimo, fila pequena de tarde", NecessidadesPerfil = "[\"cadeira_de_rodas\"]" },
            new() { LugarId = 1, DeuCerto = true, Comentario = "Rampa funcionando bem, equipe atenciosa", NecessidadesPerfil = "[\"cadeira_de_rodas\"]" },
            new() { LugarId = 1, DeuCerto = false, Comentario = "Muito barulho na sala de espera, difícil para minha filha", NecessidadesPerfil = "[\"autismo\"]" },
            new() { LugarId = 2, DeuCerto = false, Comentario = "Elevador do acesso B quebrado de novo, terceira vez esse mês", NecessidadesPerfil = "[\"cadeira_de_rodas\"]" },
            new() { LugarId = 2, DeuCerto = true, Comentario = "Acesso A funcionando, achei o caminho alternativo pela Rua Vergueiro", NecessidadesPerfil = "[\"cadeira_de_rodas\"]" },
            new() { LugarId = 3, DeuCerto = true, Comentario = "Melhor dentista para autistas que já fui. Calmo, paciente, sem pressa.", NecessidadesPerfil = "[\"autismo\"]" },
            new() { LugarId = 3, DeuCerto = true, Comentario = "Minha filha não teve crise. Recomendo de manhã, mais tranquilo.", NecessidadesPerfil = "[\"autismo\"]" },
            new() { LugarId = 3, DeuCerto = true, Comentario = "Rampa funcionando, espaço bem adaptado para cadeirantes.", NecessidadesPerfil = "[\"cadeira_de_rodas\"]" },
        });

        context.SaveChanges();
    }
}
