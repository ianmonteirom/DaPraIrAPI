# Roteiro — Vídeo de Sistemas Operacionais
## "Dá Pra Ir?" · FIAP Global Solution 2026.2 · Grupo FINELT
**Duração:** ~7 minutos · Um apresentador basta

> **Status das entregas:**
> - C# — ✅ **Pronto.** Screenshots + JSONs em `csharp/evidencias/`, README com evidências.
> - OS — ❌ **Falta gravar o vídeo.** Roteiro abaixo.
> - Pitch — ❌ **Falta gravar.** Ver `roteiro-video.md`.

---

## Preparação antes de gravar

```bash
# Parar e remover o serviço (para demonstrar do zero)
wsl -u root -e bash -c "systemctl stop dapraIr; systemctl disable dapraIr; rm -f /etc/systemd/system/dapraIr.service; systemctl daemon-reload"

# Remover diretórios criados pelo 01-infra.sh
wsl -u root -e bash -c "rm -rf /opt/dapraIr /var/log/dapraIr /etc/dapraIr.env; userdel -r dapraIr 2>/dev/null; hostnamectl set-hostname ubuntu"
```

> Publicar os binários antes:
> ```
> dotnet publish csharp\DaPraIr.API -c Release -o /tmp/dapraIr-pub
> ```

---

## 00:00 → 00:45 | Abertura

> **[Câmera no rosto, terminal WSL aberto atrás]**

**APRESENTADOR:**
> *"Olá! Sou [nome], do grupo FINELT. Neste vídeo vou demonstrar a entrega de Sistemas Operacionais do projeto Dá Pra Ir? — nossa plataforma de acessibilidade urbana para PCDs.*
>
> *Vou mostrar ao vivo três scripts Bash que provisionam um servidor Linux do zero, fazem o deploy da API e monitoram o ambiente automaticamente."*

```bash
whoami                                        # root
uname -a                                      # Linux, versão do kernel
hostname                                      # nome antes do script
ls /opt/                                      # pasta vazia
systemctl list-units | grep dapraIr           # nenhum serviço ainda
```

---

## 00:45 → 02:30 | Script 01 — Provisionamento

```bash
bash /mnt/d/Estudos/FIAP/3\ Ano/2\ semestre/gs/os/scripts/01-infra.sh
```

**APRESENTADOR (voz over enquanto o script roda):**
> *"Configurando o hostname para 'dapraIr-api'..."*

> *"Instalando pacotes: curl, git, htop, ufw e fail2ban..."*

> *"Criando o usuário de serviço 'dapraIr' — sem shell, sem senha. Se a aplicação for comprometida, o atacante não consegue fazer login."*

> *"UFW bloqueando tudo e abrindo só SSH e porta 5000. Fail2ban para detectar força bruta."*

```bash
hostname                  # dapraIr-api
ls /opt/dapraIr/          # app/ data/
id dapraIr                # usuário sem shell
ufw status                # SSH e 5000 liberados
systemctl status fail2ban
```

---

## 02:30 → 04:30 | Script 02 — Deploy

```bash
bash /mnt/d/Estudos/FIAP/3\ Ano/2\ semestre/gs/os/scripts/02-app.sh /tmp/dapraIr-pub
```

**APRESENTADOR (voz over):**
> *"Instalando o .NET 8 pelo repositório oficial da Microsoft — idempotente, pode rodar várias vezes."*

> *"Copiando binários para /opt/dapraIr/app via rsync — só o que mudou."*

> *"Criando /etc/dapraIr.env com as variáveis de ambiente. Instalando o serviço systemd. 'Restart=always' — se a API cair, o sistema reinicia em 5 segundos."*

```bash
systemctl status dapraIr            # active (running)
cat /etc/dapraIr.env                # variáveis de ambiente
cat /etc/systemd/system/dapraIr.service
journalctl -u dapraIr -n 20
curl -s http://localhost:5000/api/lugares | head -c 400
```

> *"API respondendo no Linux. Banco SQLite populado com seed data automático."*

---

## 04:30 → 05:30 | systemd ao vivo

```bash
# Demonstrar auto-restart
systemctl stop dapraIr
systemctl status dapraIr            # inactive
sleep 6
systemctl status dapraIr            # active novamente (Restart=always)
```

> *"'Restart=always' com RestartSec=5s — o systemd reiniciou automaticamente sem nenhuma intervenção."*

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

> *"Dois arquivos de log separados: relatório completo e somente alertas. Com --daemon ele monitora continuamente a cada 60 segundos."*

---

## 06:45 → 07:00 | Fechamento

> **[Câmera no rosto]**

**APRESENTADOR:**
> *"Três scripts Bash cobrindo o ciclo completo: provisionamento, deploy e monitoramento. Com systemd garantindo disponibilidade contínua. Tudo automatizado, tudo rastreável pelos logs. Obrigado!"*

---

## Requisitos técnicos — onde aparecem no vídeo

| Requisito | Script / Momento |
|---|---|
| Provisionamento automatizado | `01-infra.sh` ao vivo |
| Variáveis e parâmetros | Visíveis no topo de cada script |
| Estruturas de controle (`if`, `while`, `for`) | `03-monitor.sh` |
| Funções (`log`, `alerta`, `verificar_cpu`…) | `03-monitor.sh` |
| Redirecionamento (`tee -a`) | Logs gravados em dois arquivos |
| Serviço systemd | `02-app.sh` + `systemctl status` |
| Monitoramento de recursos | `03-monitor.sh` ao vivo |
| Segurança | UFW, fail2ban, usuário sem shell, `ProtectSystem` |

---

## Dicas de gravação

| Dica | Detalhe |
|---|---|
| **Fonte** | 16–18px no terminal |
| **Pausas** | 2–3 segundos em cada output relevante |
| **Duração** | Entre 6:30 e 7:30 está ótimo |
| **Microfone** | Testar áudio antes |
| **Legendas** | Adicionar legenda PT-BR no YouTube (auto + correção) |
