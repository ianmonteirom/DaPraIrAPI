# Prompts utilizados com IA — Global Solution 2026.2

**Projeto:** Dá Pra Ir?  
**Grupo:** FINELT  
**Ferramenta de IA:** Claude (Anthropic) via Claude Code

---

## Metodologia

A IA foi utilizada como **assistente de desenvolvimento**, com instruções precisas sobre o contexto do projeto. Todos os prompts partiram de decisões de design e arquitetura tomadas pelo grupo. O código gerado foi revisado, testado e validado pelos integrantes.

---

## Prompts principais

### 1. Definição do projeto e escopo

> "Leia nossa ideia: [descrição do Dá Pra Ir?] e o grupo é esse: FINELT [nomes e RMs]. Preciso desenvolver 3 entregas: C# (.NET 8 Web API), Sistemas Operacionais (Bash + systemd) e o roteiro do vídeo de apresentação."

**Resultado:** Planejamento completo das 3 entregas com mapeamento dos requisitos da disciplina.

---

### 2. Modelagem das entidades C#

> "Crie as entidades do projeto: Lugar (nome, tipo, endereço, bairro, coordenadas), Ficha de acessibilidade (rampa, elevador, banheiro adaptado, nível de barulho, horários tranquilos, passo a passo), Relato (deu certo?, comentário anônimo, perfil de necessidade) e Aviso (alerta temporário como elevador quebrado)."

**Resultado:** 4 entidades com relacionamentos 1:1 (Lugar→Ficha) e 1:N (Lugar→Relatos, Lugar→Avisos), usando navegação EF Core.

---

### 3. Lógica de busca por perfil com LINQ

> "Implemente o método BuscarPorPerfilAsync que recebe UsaCadeira (bool), SensibilidadeBarulho (bool), NecessitaAutismo (bool) e Bairro (string). Use LINQ para filtrar os lugares e ordene pelo percentual de relatos bem-sucedidos."

**Resultado:** Service com LINQ dinâmico encadeando `.Where()` condicionais e cálculo de `taxaSucesso` como porcentagem de relatos com `DeuCerto == true`.

---

### 4. Seed data com lugares reais de SP

> "Crie o seed data com 3 lugares reais de São Paulo representando diferentes tipos: uma UBS (saúde), uma estação de metrô (transporte) e uma clínica odontológica (saúde). Inclua fichas de acessibilidade realistas e relatos variados para demonstrar a taxa de sucesso."

**Resultado:** `SeedData.cs` com UBS Vila Mariana, Estação Paraíso (Metrô) e Clínica Sorrir Para Todos, com fichas e relatos pré-populados.

---

### 5. Scripts Bash de provisionamento

> "Crie um script Bash 01-infra.sh para provisionar um servidor Linux Ubuntu com: configuração de hostname, instalação de pacotes essenciais, criação de usuário de serviço sem shell, criação de diretórios do app, configuração de UFW (liberar SSH e porta 5000) e habilitação do fail2ban. Use funções, log com timestamp e cores no terminal."

**Resultado:** `01-infra.sh` com função `log()`, variáveis de configuração no topo, estruturas `if/for`, redirecionamento para arquivo de log.

---

### 6. Script de deploy com systemd

> "Crie 02-app.sh para instalar o .NET 8 SDK a partir do repositório oficial Microsoft, copiar os binários publicados para /opt/dapraIr/app via rsync, criar um arquivo de variáveis de ambiente em /etc/dapraIr.env, instalar o serviço systemd e iniciá-lo verificando o status."

**Resultado:** `02-app.sh` instalando .NET 8.0.428 e service unit via `systemctl enable --now`.

---

### 7. Script de monitoramento com alertas

> "Crie 03-monitor.sh que monitore CPU (alerta >85%), memória (alerta >90%) e todos os pontos de montagem de disco (alerta >80%). Se o serviço dapraIr estiver inativo, reinicie automaticamente. Grave logs separados: um completo e um só de alertas. Suporte --daemon para loop contínuo a cada 60s."

**Resultado:** `03-monitor.sh` usando `/proc/meminfo`, `top -bn1`, `df -h`, `systemctl is-active`, com saída em dois arquivos via `tee -a`.

---

### 8. Unit file systemd seguro

> "Crie o arquivo dapraIr.service com Restart=always, RestartSec=5s, carregamento de variáveis do /etc/dapraIr.env, limite de memória de 512MB e hardening de segurança: NoNewPrivileges, ProtectSystem=strict, PrivateTmp."

**Resultado:** `systemd/dapraIr.service` com todas as proteções e `WantedBy=multi-user.target`.

---

### 9. Roteiro do vídeo de apresentação

> "Crie um roteiro de apresentação de 4 minutos para o projeto, sem nenhum termo técnico (sem C#, sem Bash, sem systemd). Foco nas personas reais (Fernanda cadeirante, Gustavo autista, casal com bebê), no fluxo do app, nos dados de impacto e nas ODS. Estilo pitch moderno, dividido por integrante."

**Resultado:** `roteiro-video.md` com 6 seções (abertura, problema, solução, fluxo, impacto, fechamento), cada uma com direções de câmera e falas atribuídas por integrante.

---

### 10. Configuração do ambiente Linux (WSL2)

> "Configure o ambiente WSL2 Ubuntu 26.04 para rodar os scripts como evidência. Execute os 3 scripts em sequência e verifique que o serviço está rodando e os logs foram gerados."

**Resultado:** API Dá Pra Ir? rodando em `http://172.29.15.119:5000/swagger` no WSL2, serviço `active (running)`, logs em `/var/log/dapraIr/`.

---

## Contribuição humana vs. IA

| Decisão | Responsável |
|---|---|
| Nome e conceito do projeto "Dá Pra Ir?" | Grupo FINELT |
| Escolha das personas (Fernanda, Gustavo, casal com bebê) | Grupo FINELT |
| Requisitos de acessibilidade das fichas | Grupo FINELT |
| Arquitetura da API (entidades, relacionamentos) | Grupo + IA |
| Implementação dos scripts e código | IA com revisão do grupo |
| Dados de seed (lugares reais de SP) | Grupo (IA executou) |
| Validação e testes do sistema | Grupo FINELT |
