# Roteiro — Vídeos Técnicos
## "Dá Pra Ir?" · FIAP Global Solution 2026.2 · Grupo FINELT
**Carlos (558003) · Mauricio (556214) · Ian (558652) · Bruno (550416) · João (550763)**

> **Atenção:** O vídeo com múltiplos apresentadores é só o pitch do projeto (Testing, 4 min) — ver `roteiro-video.md`.  
> OS exige vídeo de até 7 min (um apresentador basta).  
> C# **não exige vídeo** — só "evidências da aplicação" (screenshots do Swagger no README).

---

# PARTE 1 — C# · Evidências (sem vídeo obrigatório)

> O PDF de C# pede apenas *"Evidências da aplicação"* junto ao repositório GitHub.  
> **Opção A (recomendada):** adicionar screenshots do Swagger ao README.  
> **Opção B:** gravar um vídeo curto (3–5 min) se quiser caprichar. Roteiro abaixo.

## Screenshots mínimas para o README (Opção A)

Tire print de cada uma dessas telas no Swagger (`http://localhost:5000/swagger`):

1. **Tela inicial do Swagger** — com todos os endpoints listados
2. **GET /api/lugares?usaCadeira=true** — request + response com os lugares filtrados
3. **GET /api/lugares/1** — detalhe completo com ficha de acessibilidade
4. **POST /api/lugares/1/relatos** — body da requisição + 200 OK
5. **POST /api/lugares/2/avisos** — body da requisição + 200 OK

Adicione as imagens na pasta `csharp/evidencias/` e referencie no README com `![descrição](evidencias/nome.png)`.

---

## Roteiro vídeo curto — C# (Opção B) · ~5 minutos

### Preparação

```bash
cd D:\Estudos\FIAP\3 Ano\2 semestre\gs\csharp\DaPraIr.API
del dapraIr.db   # banco limpo para o seed rodar do zero
dotnet build     # confirmar 0 erros
```

### 00:00 → 00:30 | Abertura

> **[Câmera no rosto]**  
> *"Vou demonstrar a API Dá Pra Ir? em C# .NET 8 — entrega de Software Development."*

### 00:30 → 01:30 | Estrutura do projeto

> **[VS Code — estrutura de pastas]**

> **[Abrir `Models/Lugar.cs`]** — *"Entidade central com relacionamentos 1:1 (Ficha) e 1:N (Relatos, Avisos)."*

> **[Abrir `Services/LugarService.cs`]** — *"LINQ dinâmico com encadeamento de filtros por perfil de acessibilidade. async/await em todos os métodos."*

> **[Abrir `DTOs/LugarDto.cs`]** — *"Record DTOs para transferência de dados sem expor as entidades."*

### 01:30 → 02:00 | Subir a API

```bash
dotnet run
```
> *"EF Core criando o banco SQLite e populando com seed data automático — 3 lugares reais de SP."*

> **[Abrir `http://localhost:5000/swagger`]**

### 02:00 → 04:30 | 5 chamadas ao vivo

**Busca por cadeirante:**
```
GET /api/lugares?usaCadeira=true&bairro=Vila Mariana
```

**Busca para autismo:**
```
GET /api/lugares?necessitaAutismo=true&sensibilidadeBarulho=true
```

**Detalhe completo:**
```
GET /api/lugares/1
```

**Relato anônimo:**
```json
POST /api/lugares/1/relatos
{"deuCerto": true, "comentario": "Rampa funcionando", "necessidadesPerfil": ["cadeira_de_rodas"]}
```

**Aviso em tempo real:**
```json
POST /api/lugares/2/avisos
{"titulo": "Elevador em manutenção", "descricao": "Use saída Vergueiro.", "tipo": "manutencao"}
```

### 04:30 → 05:00 | Fechamento

> *"API .NET 8 com EF Core, LINQ, async/await, injeção de dependência e Record DTOs. Obrigado!"*

---

## Requisitos técnicos — onde aparecem

| Requisito | Momento |
|---|---|
| POO (3,0 pts) | Entidades, interface ILugarService → LugarService |
| Web API (2,5 pts) | Controllers com rotas REST, Swagger |
| EF Core + LINQ (1,5 pts) | DbContext, BuscarPorPerfilAsync |
| Async/Await (1,0 pts) | Assinatura de todos os métodos |
| Qualidade do código (2,0 pts) | DTOs, DI, seed data, XML docs |

---
---

# PARTE 2 — OS (Sistemas Operacionais · Bash + systemd) · ~7 minutos

## Preparação antes de gravar

```bash
cd D:\Estudos\FIAP\3 Ano\2 semestre\gs\csharp\DaPraIr.API

# Remover o banco para o seed rodar do zero
del dapraIr.db

# Confirmar que compila sem erros
dotnet build
```

> Deixar o VS Code aberto com a estrutura de pastas visível na sidebar.

---

## 00:00 → 00:40 | Abertura

> **[Câmera no rosto]**

**APRESENTADOR:**
> *"Olá! Sou [nome], do grupo FINELT. Neste vídeo vou demonstrar a entrega de Desenvolvimento em C# do projeto Dá Pra Ir? — nossa plataforma de acessibilidade urbana para PCDs e famílias atípicas.*
>
> *Vou mostrar a estrutura da API, os requisitos técnicos implementados e a aplicação rodando ao vivo com chamadas reais."*

---

## 00:40 → 02:00 | Estrutura do projeto

> **[Tela: VS Code com estrutura de pastas]**

**APRESENTADOR:**
> *"Em Models temos as quatro entidades do domínio..."*

> **[Abrir `Models/Lugar.cs`]**
> *"Lugar é a entidade central — nome, tipo, endereço, coordenadas e relacionamentos com as outras três entidades."*

> **[Abrir `Models/Ficha.cs`]**
> *"Ficha é a ficha de acessibilidade — rampa, elevador, banheiro adaptado, nível de barulho e passo a passo. Relacionamento um para um com Lugar."*

> **[Abrir `Models/Relato.cs`]**
> *"Relato é o feedback anônimo pós-visita — só 'deu certo?' e um comentário opcional. Zero dados pessoais, zero login."*

> **[Abrir `Models/Aviso.cs`]**
> *"Aviso é o alerta em tempo real — 'elevador quebrado hoje'. Tem data de expiração para sumir automaticamente."*

> **[Mostrar pasta DTOs]**
> *"Em DTOs estão os Record types do C# — tipos imutáveis para transferência de dados entre camadas, sem expor as entidades diretamente."*

> **[Mostrar pasta Services]**
> *"E em Services temos a interface ILugarService e sua implementação — aplicando injeção de dependência e separação de responsabilidades."*

---

## 02:00 → 03:30 | Código — LINQ e async/await

> **[Abrir `Services/LugarService.cs`]**

**APRESENTADOR:**
> *"O método BuscarPorPerfilAsync concentra vários requisitos técnicos. Usamos LINQ com encadeamento dinâmico de filtros — cada condição do perfil do usuário adiciona um Where na query. O EF Core traduz tudo em uma única consulta SQL eficiente."*

> **[Destacar `async Task`]**
> *"Todos os métodos são assíncronos com async/await — a thread do servidor não fica bloqueada esperando o banco, permitindo atender múltiplas requisições simultaneamente."*

> **[Mostrar o cálculo de taxa de sucesso]**
> *"E aqui calculamos a taxa de sucesso: porcentagem de relatos onde 'deu certo' é verdadeiro. Os resultados são ordenados por essa taxa — quem foi antes ajuda quem vai depois."*

---

## 03:30 → 04:00 | Subir a API ao vivo

> **[Tela: terminal integrado do VS Code]**

```bash
dotnet run
```

> *"O EF Core criando o banco de dados... SeedData populando com 3 lugares reais de SP. API disponível na porta 5000."*

> **[Abrir `http://localhost:5000/swagger`]**

---

## 04:00 → 06:30 | Demonstração — 5 chamadas ao vivo

> **[Tela: Swagger no browser]**

**Chamada 1 — Busca por perfil de cadeirante**
```
GET /api/lugares?usaCadeira=true&bairro=Vila Mariana
```
> *"UBS Vila Mariana com 100% de taxa de sucesso. Campos 'temRampa' e 'temBanheiroAdaptado' presentes na resposta."*

**Chamada 2 — Busca para autismo**
```
GET /api/lugares?necessitaAutismo=true&sensibilidadeBarulho=true
```
> *"Clínica Sorrir Para Todos — nível de barulho 'baixo' e 'atendeAutismo' verdadeiro."*

**Chamada 3 — Detalhe completo**
```
GET /api/lugares/1
```
> *"Ficha completa: passo a passo, horários tranquilos, avisos ativos e taxa de sucesso calculada."*

**Chamada 4 — Registrar relato**
```json
POST /api/lugares/1/relatos
{
  "deuCerto": true,
  "comentario": "Rampa funcionando perfeitamente, atendimento ótimo",
  "necessidadesPerfil": ["cadeira_de_rodas"]
}
```
> *"Relato registrado. Anônimo, sem login, em um clique."*

**Chamada 5 — Criar aviso**
```json
POST /api/lugares/2/avisos
{
  "titulo": "Elevador principal em manutenção",
  "descricao": "Use o elevador da saída Vergueiro. Previsão: até 18h.",
  "tipo": "manutencao"
}
```
> *"Aviso criado. Qualquer usuário que consultar essa estação agora vai ver esse alerta antes de sair de casa."*

---

## 06:30 → 07:00 | Fechamento

> **[Câmera no rosto]**

**APRESENTADOR:**
> *"Uma API REST .NET 8 com EF Core, LINQ dinâmico, async/await, injeção de dependência, Record DTOs e seed data automático — cobrindo todos os requisitos da disciplina. Obrigado!"*

---

## Requisitos técnicos — onde aparecem no vídeo

| Requisito | Momento |
|---|---|
| EF Core 8 com SQLite | DbContext + banco criado ao subir |
| LINQ com filtros | LugarService.cs — BuscarPorPerfilAsync |
| async/await | Assinatura de todos os métodos |
| POO — interface/implementação | ILugarService → LugarService |
| Record DTOs | DTOs/LugarDto.cs na sidebar |
| Injeção de dependência | Mencionar Program.cs |
| Seed Data | Log do terminal ao subir |
| XML Documentation | Swagger com descrições nos endpoints |

---
---

# PARTE 2 — OS (Sistemas Operacionais · Bash + systemd) · ~7 minutos

## Preparação antes de gravar

```bash
# Parar e remover o serviço
wsl -u root -e bash -c "systemctl stop dapraIr; systemctl disable dapraIr; rm -f /etc/systemd/system/dapraIr.service; systemctl daemon-reload"

# Remover diretórios e usuário criados pelo 01-infra.sh
wsl -u root -e bash -c "rm -rf /opt/dapraIr /var/log/dapraIr /etc/dapraIr.env; userdel -r dapraIr 2>/dev/null; hostnamectl set-hostname ubuntu"

# Remover .NET (opcional — reinstalar demonstra mais, mas demora ~2 min)
# wsl -u root -e bash -c "apt-get remove -y dotnet-sdk-8.0 2>/dev/null"
```

> Deixar os arquivos dos scripts visíveis na sidebar para mostrar o código antes de executar.

---

## 00:00 → 00:45 | Abertura

> **[Câmera no rosto, terminal WSL aberto atrás]**

**APRESENTADOR:**
> *"Olá! Sou [nome], do grupo FINELT. Neste vídeo vou demonstrar a entrega de Sistemas Operacionais do projeto Dá Pra Ir?.*
>
> *Vou mostrar ao vivo três scripts Bash que provisionam um servidor Linux do zero, fazem o deploy da nossa API e monitoram o ambiente automaticamente."*

> **[Alterna para terminal em tela cheia]**

```bash
whoami           # root
uname -a         # Linux, versão do kernel
hostname         # nome padrão antes do script
ls /opt/         # pasta vazia
systemctl list-units | grep dapraIr  # nenhum serviço
```

---

## 00:45 → 02:30 | Script 01 — Provisionamento de infraestrutura

```bash
bash /mnt/d/Estudos/FIAP/3\ Ano/2\ semestre/gs/os/scripts/01-infra.sh
```

**APRESENTADOR (voz over enquanto o script roda):**
> *"Configurando o hostname para 'dapraIr-api'..."*

> *"Atualizando pacotes e instalando dependências: curl, git, htop, ufw e fail2ban..."*

> *"Criando o usuário de serviço 'dapraIr' — sem shell e sem senha. Se a aplicação for comprometida, o atacante não consegue fazer login no servidor."*

> *"Criando diretórios: /opt/dapraIr/app para binários, /opt/dapraIr/data para o banco e /var/log/dapraIr para os logs."*

> *"UFW bloqueando todas as conexões de entrada e abrindo apenas SSH e porta 5000. Fail2ban habilitado para detectar ataques de força bruta."*

```bash
hostname                # dapraIr-api
ls /opt/dapraIr/        # app/ data/
id dapraIr              # usuário sem shell
ufw status              # SSH e 5000 liberados
systemctl status fail2ban
```

---

## 02:30 → 04:30 | Script 02 — Deploy da aplicação

```bash
bash /mnt/d/Estudos/FIAP/3\ Ano/2\ semestre/gs/os/scripts/02-app.sh /tmp/dapraIr-pub
```

**APRESENTADOR (voz over):**
> *"O script verifica se o .NET 8 já está instalado. Se não estiver, adiciona o repositório oficial da Microsoft e instala. Idempotente — pode ser rodado várias vezes sem problema."*

> *"Copiando binários para /opt/dapraIr/app via rsync — sincroniza só os arquivos alterados."*

> *"Criando /etc/dapraIr.env com as variáveis de ambiente: string de conexão e configurações do servidor."*

> *"Instalando o serviço systemd. 'Restart=always' — se a API cair, o sistema operacional reinicia em 5 segundos."*

```bash
systemctl status dapraIr         # active (running)
cat /etc/dapraIr.env             # variáveis de ambiente
cat /etc/systemd/system/dapraIr.service
```

---

## 04:30 → 05:30 | Verificação do serviço systemd

```bash
systemctl status dapraIr
journalctl -u dapraIr -n 30
curl -s http://localhost:5000/api/lugares | head -c 500
```

> *"Logs do systemd: aplicação iniciada, banco conectado, porta 5000 ouvindo. O curl retornou os dados — API funcionando no Linux."*

> **[Abrir browser em `http://172.29.15.119:5000/swagger`]**

```
GET /api/lugares?usaCadeira=true
```

---

## 05:30 → 06:45 | Script 03 — Monitoramento

```bash
bash /mnt/d/Estudos/FIAP/3\ Ano/2\ semestre/gs/os/scripts/03-monitor.sh
```

> *"CPU 1%, memória 8%. As partições do Windows aparecem acima de 80% — o script detectou e gerou alertas, que é exatamente o comportamento esperado."*

```bash
cat /var/log/dapraIr/monitor.log
cat /var/log/dapraIr/alertas.log
```

> *"Dois arquivos de log: relatório completo e somente alertas. Com --daemon ele monitora continuamente a cada 60 segundos."*

---

## 06:45 → 07:00 | Fechamento

> **[Câmera no rosto]**

**APRESENTADOR:**
> *"Três scripts Bash cobrindo o ciclo completo: provisionamento, deploy e monitoramento. Com systemd garantindo disponibilidade contínua. Tudo automatizado, tudo rastreável pelos logs. Obrigado!"*

---

## Requisitos técnicos — onde aparecem no vídeo

| Requisito | Script / Momento |
|---|---|
| Provisionamento automatizado | 01-infra.sh ao vivo |
| Variáveis e parâmetros | Visíveis no topo de cada script |
| Estruturas de controle (if, while, for) | 03-monitor.sh |
| Funções (log, alerta, verificar_cpu…) | 03-monitor.sh |
| Redirecionamento (tee -a) | Logs gravados em dois arquivos |
| Serviço systemd | 02-app.sh + systemctl status |
| Monitoramento de recursos | 03-monitor.sh ao vivo |
| Segurança | UFW, fail2ban, usuário sem shell, ProtectSystem |

---

## Dicas gerais de gravação (para ambos os vídeos)

| Dica | Detalhe |
|---|---|
| **Fonte** | 16–18px no terminal e IDE |
| **Zoom Swagger** | 125% para ficar legível no vídeo |
| **JSON prontos** | Copiar os exemplos antes — sem digitar ao vivo |
| **Pausas** | 2–3 segundos em cada output relevante |
| **Microfone** | Testar áudio antes de começar |
| **Duração** | Entre 6:30 e 7:30 está ótimo para cada vídeo |
