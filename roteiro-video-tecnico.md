# Roteiro — Vídeo OS
## "Dá Pra Ir?" · FIAP Global Solution 2026.2 · Grupo FINELT
**Duração:** ~3 minutos · Um apresentador basta

> **Status:**
> - C# ✅ Pronto (evidências em `csharp/evidencias/`)
> - OS ❌ Gravar este vídeo
> - Pitch ❌ Gravar (`roteiro-video.md`)

---

## Preparação (antes de ligar a câmera)

```bash
# Limpar tudo para demonstrar do zero
wsl -u root -e bash -c "systemctl stop dapraIr 2>/dev/null; rm -f /etc/systemd/system/dapraIr.service; systemctl daemon-reload; rm -rf /opt/dapraIr /var/log/dapraIr /etc/dapraIr.env; userdel -r dapraIr 2>/dev/null; hostnamectl set-hostname ubuntu"

# Publicar binários
dotnet publish csharp\DaPraIr.API -c Release -o C:\Temp\dapraIr-pub
```

---

## 00:00 → 00:30 | Abertura + mostrar o código

> *"Olá, sou [nome], grupo FINELT. Vou demonstrar a entrega de Sistemas Operacionais do projeto Dá Pra Ir? — três scripts Bash que provisionam, fazem deploy e monitoram a API no Linux."*

```bash
# Mostrar variáveis, funções e estruturas de controle de uma vez
grep -n "^[A-Z_]*=" os/scripts/01-infra.sh | head -10     # variáveis
grep -n "^function\|^[a-z_]*() {" os/scripts/03-monitor.sh # funções
grep -n "if \|while \|for " os/scripts/03-monitor.sh | head -8  # controle
```

> *"Variáveis de configuração no topo, funções reutilizáveis, estruturas de controle para lógica de alerta."*

---

## 00:30 → 01:10 | Script 01 — Provisionamento

```bash
wsl -u root -e bash /mnt/d/Estudos/FIAP/3\ Ano/2\ semestre/gs/os/scripts/01-infra.sh
```

> *"Hostname, pacotes, usuário de serviço sem shell, diretórios, UFW e fail2ban."*

```bash
wsl hostname && wsl id dapraIr && wsl ufw status
```

---

## 01:10 → 02:00 | Script 02 — Deploy + systemd

```bash
wsl -u root -e bash /mnt/d/Estudos/FIAP/3\ Ano/2\ semestre/gs/os/scripts/02-app.sh /mnt/c/Temp/dapraIr-pub
```

```bash
wsl systemctl status dapraIr         # active (running)
wsl cat /etc/systemd/system/dapraIr.service
```

> *"Restart=always, ProtectSystem=strict, MemoryMax=512M — serviço seguro com reinício automático."*

```bash
# Demonstrar auto-restart
wsl -u root -e bash -c "systemctl stop dapraIr; sleep 6; systemctl status dapraIr"
```

> *"Parei o serviço — o systemd reiniciou sozinho em 5 segundos."*

---

## 02:00 → 02:40 | Script 03 — Monitoramento + logs

```bash
wsl -u root -e bash /mnt/d/Estudos/FIAP/3\ Ano/2\ semestre/gs/os/scripts/03-monitor.sh
```

```bash
wsl cat /var/log/dapraIr/monitor.log
wsl cat /var/log/dapraIr/alertas.log
```

> *"Redirecionamento com tee -a: dois arquivos de log separados — relatório completo e só alertas."*

---

## 02:40 → 03:00 | Fechamento

> *"Provisionamento, deploy e monitoramento automatizados. systemd garantindo disponibilidade. Obrigado!"*

---

## Rubrica — confirmação dos 8 itens

| Requisito | Aparece em |
|---|---|
| Variáveis e parâmetros | `grep` nas variáveis do 01-infra.sh |
| Funções | `grep` nas funções do 03-monitor.sh |
| Estruturas de controle | `grep` nos `if/while/for` do 03-monitor.sh |
| Redirecionamento | `tee -a` no log + dois arquivos mostrados |
| Serviço systemd | `systemctl status` + `.service` file na tela |
| Auto-restart | stop → 6s → status active |
| Monitoramento CPU/RAM/disco | output do 03-monitor.sh |
| Segurança | `ufw status` + `ProtectSystem` no service file |
