# SaraEArtur.API
API para gerenciamento de presentes e pedidos, desenvolvida para o casamento de Sara e Artur.
## 🚧 Status do Projeto
Em desenvolvimento 🚧
Funcionalidades, endpoints e documentação estão sujeitos a alterações.
## 📋 Funcionalidades
- [x] Configuração de projeto ASP.NET Core 8 com PostgreSQL
- [x] Integração básica do Entity Framework Core
- [x] Swagger para documentação automática
- [ ] Gerenciamento de presentes (`/gifts`)
- [ ] Gerenciamento de pedidos (`/orders`)
- [ ] Atualização de status dos presentes e pedidos

## 🚀 Como Executar o Projeto
1. **Pré-requisitos**
    - [.NET 8 SDK](https://dotnet.microsoft.com/download)
    - [PostgreSQL](https://www.postgresql.org/download/)

2. **Clone o repositório e configure seu banco de dados**
3. **Defina a string de conexão:**
   No arquivo , configure a propriedade `DefaultConnection` com os dados do seu banco PostgreSQL. `appsettings.json`
4. **Execute as migrations e rode a aplicação**
``` bash
   dotnet ef database update
   dotnet run --project server/SaraEArtur.API/SaraEArtur.API.csproj
```
1. **Acesse a documentação no navegador:**
``` 
   http://localhost:5000/swagger
```
_(A porta pode variar — confira o terminal ao rodar o projeto)_
## 📂 Estrutura do Projeto
``` 
/server
  /SaraEArtur.API
    Program.cs
    Data/
    Routes/
  ...
```
## 📑 Documentação dos Endpoints
A documentação detalhada dos endpoints estará disponível via Swagger (`/swagger`) assim que eles forem implementados.
## 📝 To-Do
- Finalizar endpoints de presentes e pedidos
- Escrever testes básicos
- Adicionar exemplos de uso para cada rota
- Melhorar esta documentação
