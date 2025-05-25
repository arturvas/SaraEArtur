# 💍 Sara & Artur — Wedding Site

> **Um SPA em Angular + .NET 8 pensado para telas pequenas e pagamento simbólico de presentes via MercadoPago.**

## ✨ Funcionalidades

* **Home** com contagem regressiva animada e atalho para todas as seções.
* **História dos noivos** em linha do tempo, 100 % mobile‑first.
* **Localização** integrada ao Google Maps com rota 1‑clique.
* **Dress Code** ilustrado.
* **Lista de Presentes** (catálogo) → reserva → checkout MercadoPago (cartão ou PIX).
* **Confirmação de Presença** opcional (magic‑link por e‑mail).
* **Dashboard Admin** minimal para acompanhar pagamentos.

## 🛠️ Stack

| Camada          | Tecnologia                            |
| --------------- | ------------------------------------- |
| Frontend SPA    | **Angular 18** · **Tailwind CSS 3**   |
| Backend API     | **ASP.NET Core 8 Minimal API**        |
| Banco de Dados  | **PostgreSQL 15** (Azure DB)          |
| Pagamentos      | **MercadoPago Checkout Pro**          |
| Auth (opcional) | Magic‑link (SendGrid) ou Social Login |
| Hospedagem      | Azure Static Web Apps + App Service   |
| CI/CD           | GitHub Actions                        |

## 🖼️ Arquitetura em alto nível

```
  ┌────────────────┐        https        ┌────────────────────┐
  │  Angular SPA   │ ──────────────────► │ ASP.NET Minimal API│
  └────────────────┘                     │  (Docker / Linux)  │
          ▲                              └─────┬────────┬─────┘
          │CORS                                │EF Core │Webhook
          │                                    ▼        ▼
   ┌───────────────┐       tcp/5432      ┌────────────────────┐
   │   Browser     │ ◄────────────────── │  PostgreSQL 15     │
   └───────────────┘                     └────────────────────┘
                                ▲  Webhook
                                │
                      ┌────────────────────┐
                      │ MercadoPago API    │
                      └────────────────────┘
```

## 📁 Estrutura de pastas

```
SaraEArtur/
├─ client/                # Angular + Tailwind
│  └─ src/
├─ server/                # .NET 8 Minimal API
│  └─ SaraEArtur.API/
├─ docs/                  # screenshots, diagramas
└─ .github/workflows/     # CI/CD
```

## 🚀 Como rodar localmente

### 1. Pré‑requisitos

* Node 20 LTS + `npm`
* Angular CLI v18 global: `npm i -g @angular/cli`
* .NET SDK 8.0
* PostgreSQL ≥ 15 (local ou Docker)

### 2. Variáveis de Ambiente

Crie `server/.env` (ou use Secret Manager):

```env
ConnectionStrings__Default=Host=localhost;Port=5432;Database=wedding;Username=postgres;Password=postgres
MercadoPago__AccessToken=TEST-xxxxxxxxxxxxxxxxxxxx
MercadoPago__PublicKey=TEST-xxxxxxxxxxxxxxxxxxxx
FrontEnd__BaseUrl=http://localhost:4200
```

### 3. Backend

```bash
cd server/src/SaraEArtur.API
# restaura pacotes & aplica migrations
dotnet build
dotnet ef database update
# roda a API com hot‑reload
DOTNET_ENVIRONMENT=Development dotnet watch run
```

API disponível em `https://localhost:5080/swagger`.

### 4. Frontend

```bash
cd client
npm ci
ng serve -o
```

SPA em [http://localhost:4200](http://localhost:4200).

## 🧪 Scripts úteis

| Ação                   | Comando                               |
| ---------------------- | ------------------------------------- |
| Aplicar nova migration | `dotnet ef migrations add <Nome>`     |
| Rodar testes API       | `dotnet test`                         |
| Rodar testes Angular   | `ng test`                             |
| Build produção SPA     | `ng build --configuration production` |
| Build Docker backend   | `docker build -t wedding-api .`       |

## 📦 Deploy (Azure)

1. Crie **Static Web App** para `client/dist` (branch `main`).
2. Crie **App Service Linux** (.NET 8) para API.
3. Configure secrets `ConnectionStrings__Default` e chaves MercadoPago.
4. Pipeline GitHub Actions já faz o push a cada merge.

## 🗺️ Roadmap (MVP → v1.0)

* [x] Skeleton Angular + Tailwind
* [x] Skeleton API Minimal
* [ ] CRUD GiftItem + seed
* [ ] Checkout MercadoPago sandbox
* [ ] Webhook de confirmação
* [ ] Dashboard admin
* [ ] Autenticação magic‑link (opcional)

## 🤝 Contribuições

Pull Requests são bem‑vindos. Abra issues para bugs ou sugestões.

## 👤 Autor

**Artur Vasconcelos** — [@arturvas](https://github.com/arturvas)

## 📝 Licença

Distribuído sob licença MIT. Consulte `LICENSE` para detalhes.
