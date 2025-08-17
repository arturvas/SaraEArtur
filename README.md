# Sara e Artur

Um sistema web completo desenvolvido para o meu casamento com minha linda noiva Sara.

## Tecnologias Utilizadas

### Frontend
- **Astro** 5
- **Tailwind CSS** 4
- **Lucide Icons**
- **Vanilla JS**

### Backend
- **ASP.NET Core** 8.0
- **C#** 12.0
- **Entity Framework Core** 8 - ORM para acesso a dados
- **PostgreSQL**

### APIs e Integrações
- **MercadoPago SDK** - Gateway de pagamentos
- **Google Maps Embed API** - Localização do evento
- **Web API** - Comunicação frontend/backend

### DevOps e Deploy
- **Docker** - Containerização da aplicação
- **Vercel** - Hospedagem do frontend
- **Render** - Hospedagem da API e banco de dados
- **Docker Compose** - Orquestração de containers

## Funcionalidades

-  **Countdown para o casamento** - Contador regressivo dinâmico
-  **Lista de presentes** - Sistema de lista de presentes integrado
-  **Localização do evento** - Mapa interativo com Google Maps
-  **Integração com MercadoPago** - Pagamentos seguros
-  **Design responsivo** - Interface adaptada para todos os dispositivos
-  **Web API** - Backend para pagamento e dados

## Arquitetura

O projeto segue uma arquitetura cliente-servidor com:
-  client/wedding-frontend/ # Frontend em Astro + Vanilla JS → Vercel
-  server/Wedding.API/ # Backend em ASP.NET Core → Render
-  docker-compose.yml # Orquestração dos serviços

## Destaques Técnicos

- **Frontend moderno** com Astro para performance otimizada
- **Backend escalável** com ASP.NET Core e Entity Framework
- **Containerização** completa com Docker
- **Deploy automatizado** com CI/CD na Vercel e Render
- **Integrações externas** com MercadoPago e Google Maps
- **UX otimizada** com mapa interativo para localização
- **Produção real** - aplicação funcionando em ambiente de prod

---

*Desenvolvi com 💙 para nós, Sara e Artur.*

