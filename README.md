# Sara e Artur

No front-end, foquei em performance e leveza usando **Astro 5** com **Tailwind CSS 4**. A interatividade ficou por conta do **Vanilla JS** e os ícones com **Lucide**. É um site que carrega instantaneamente e funciona perfeitamente em qualquer dispositivo.

Já o "coração" do projeto é uma **Web API** robusta em **ASP.NET Core 8** com **C# 12**. Usei o **Entity Framework Core** para gerenciar os dados no **PostgreSQL**. Para a parte de pagamentos, integrei o **SDK do MercadoPago**, garantindo que os presentes cheguem com segurança, e usei a **API do Google Maps Embed** para ninguém se perder no caminho da festa.

### Funcionalidades e Deploy

O sistema conta com um **countdown** dinâmico, uma **lista de presentes** funcional que já cai direto no checkout seguro e um **mapa interativo**.

Toda a infraestrutura foi pensada para ser moderna. Utilizei **Docker** e **Docker Compose** para orquestrar tudo em containers. O deploy é automatizado com CI/CD: o front-end roda na **Vercel** e a API, junto com o banco de dados, está hospedada no **Render**. É uma aplicação de produção real, feita com código limpo e muito carinho.
