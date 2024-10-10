# Sistema de Cadastro de Produtos

Um sistema completo para o gerenciamento de produtos e usuários, desenvolvido com ASP.NET Core e Entity Framework Core. Este projeto permite a criação, leitura, atualização e exclusão de produtos e usuários, com suporte a autenticação JWT e logging com Serilog.

## Funcionalidades

- **Gerenciamento de Produtos**: Criação, edição, exclusão e listagem de produtos.
- **Gerenciamento de Usuários**: Criação, edição e controle de acesso de usuários.
- **Validações**: Validações robustas para dados de entrada.
- **Autenticação JWT**: Proteção das APIs com autenticação via tokens JWT.
- **Logging**: Monitoramento de eventos e erros com Serilog.

## Tecnologias Usadas

- **ASP.NET Core**: Framework para construção de aplicações web.
- **Entity Framework Core**: ORM para interagir com o banco de dados.
- **MySQL**: Sistema de gerenciamento de banco de dados.
- **JWT (JSON Web Tokens)**: Mecanismo para autenticação segura.
- **Serilog**: Biblioteca de logging para ASP.NET Core.

## Pré-requisitos

Antes de executar o projeto, verifique se você possui as seguintes ferramentas instaladas:

- [.NET SDK 6.0 ou superior](https://dotnet.microsoft.com/download)
- [MySQL](https://www.mysql.com/downloads/)
- [Visual Studio ou Visual Studio Code](https://visualstudio.microsoft.com/)

## Configuração do Banco de Dados

1. **Conexão com o Banco**:
   - Configure a string de conexão no arquivo `appsettings.json`:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=seu_servidor;Database=nome_do_banco;User=usuario;Password=senha;"
       }
     }
     ```

2. **Migrações**:
   - Execute o comando para aplicar as migrações:
     ```bash
     dotnet ef database update
     ```

## Configuração do JWT

- Configure os parâmetros do JWT no arquivo `appsettings.json`:
  ```json
  "Jwt": {
    "Key": "sua_chave_secreta",
    "Issuer": "MeuSistemaDeCadastro",
    "Audience": "MeuAppCliente"
  }

Como Executar
Clone o repositório: 
git clone https://github.com/seu_usuario/SistemaDeCadastro.git
cd SistemaDeCadastro

Restaure as dependências:
dotnet restore

Compile e inicie o projeto:
dotnet run


Estrutura do Projeto
appsettings.json
Configurações de logging, conexão com o banco de dados e JWT.
.csproj
Inclui todas as dependências do projeto, como:
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.10" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.8" />
<PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="8.0.2" />
<PackageReference Include="Serilog" Version="4.0.2" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />

Exemplos de Uso
Cadastro de Produto //lembre-se de fazer autenticação 
Requisição POST para cadastrar um novo produto:
POST /api/produtos
Content-Type: application/json

{
  "name": "Produto Exemplo",
  "quantity": 10,
  "price": 99.99,
  "validation": "2025-12-31"
}
 
Cadastro de Usuário //lembre-se de fazer autenticação  
Requisição POST para cadastrar um novo usuário:
POST /api/usuarios
Content-Type: application/json

{
  "userName": "usuario123",
  "passwordHash": "senhaSegura123",
  "acesso": "Admin"
}

