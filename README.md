# Dá Pra Ir? — Global Solution 2026.2 · CitizenTech

**FIAP Engenharia de Software · 3º Ano**  
**Grupo FINELT** — Carlos Henrique (558003) · Mauricio Alves (556214) · Ian Monteiro (558652) · Bruno Silva (550416) · João Hoffmann (550763)

---

## Ficha do Projeto — CitizenTech

| Campo | Informação |
|---|---|
| **Nome do projeto** | Dá Pra Ir? |
| **Tagline** | "Vai funcionar pra mim?" — acessibilidade urbana verificada pela comunidade |
| **Grupo** | FINELT |
| **Turma** | 3ESPX |
| **Integrantes** | Carlos (558003), Mauricio (556214), Ian (558652), Bruno (550416), João (550763) |
| **Desafio** | CitizenTech — Tecnologia para cidadania e inclusão |
| **Problema** | 18,6 milhões de PCDs no Brasil não têm como saber, antes de sair de casa, se um lugar terá as condições de acessibilidade de que precisam |
| **Solução** | App que responde "dá pra ir?" com base na ficha de acessibilidade do lugar e nos relatos anônimos de quem visitou antes |
| **Público-alvo** | PCDs (cadeirantes, pessoas com deficiência auditiva/visual), autistas, famílias atípicas, cuidadores |
| **ODS atendidas** | ODS 3 (Saúde e bem-estar), ODS 10 (Redução das desigualdades), ODS 11 (Cidades sustentáveis) |
| **Tecnologias** | .NET 8 Web API (C#), EF Core, SQLite |
| **Repositório** | https://github.com/ianmonteirom/DaPraIrAPI |

---

## O que é o Dá Pra Ir?

Antes de ir a um novo lugar, pessoas com deficiência ou com filhos autistas precisam saber: **tem rampa? O elevador funciona? É barulhento? Qual o melhor horário?**

O **Dá Pra Ir?** reúne essas informações em um único lugar, combinando fichas detalhadas de acessibilidade com relatos anônimos de quem foi antes — sem login, sem burocracia, sem expor diagnóstico.

### Fluxo principal

1. Usuário informa suas necessidades (cadeira de rodas, sensibilidade a barulho, autismo)
2. App retorna lugares ordenados pela taxa de sucesso calculada pelos relatos
3. Cada lugar tem ficha completa: rampa, elevador, banheiro adaptado, nível de barulho, passo a passo de acesso
4. Avisos em tempo real: "elevador quebrado hoje", "obra na entrada"
5. Após a visita: relato anônimo em 1 clique — "deu certo?"

---

## Estrutura do repositório

```
gs/
├── csharp/          # Entrega C# — .NET 8 Web API
│   ├── DaPraIr.API/
│   └── README.md
├── prompts.md       # Prompts utilizados com IA
└── README.md        # Este arquivo
```

---

## Entrega

| Disciplina | Pasta | Descrição |
|---|---|---|
| **Desenvolvimento em C#** | `csharp/` | API REST .NET 8 com EF Core, LINQ, async/await, POO |

---

## Como executar

```bash
cd csharp/DaPraIr.API
dotnet run
# Acesse: http://localhost:5000/swagger
```
