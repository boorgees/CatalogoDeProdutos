# Catálogo de Produtos API

## 📋 Descrição

Uma API RESTful desenvolvida em ASP.NET Core para gerenciamento de catálogo de produtos. O sistema permite o
gerenciamento de produtos e suas categorias, oferecendo operações CRUD completas. Estes códigos fazem parte de uma longa
e minuciosa rotina de estudos sobre API's REST e de boas práticas.

## 🚀 Tecnologias Utilizadas

- .NET 9.0
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Swagger
- AutoMapper

## 🛠 Recursos

- Gerenciamento completo de Produtos
- Gerenciamento de Categorias
- Mapeamento automático entre DTOs e Entidades
- Documentação da API com Swagger
- Padrão Repository Pattern
- Unit of Work
- Injeção de Dependência

## 🏗 Arquitetura

O projeto segue uma arquitetura em camadas:

- **Controllers**: Responsáveis pelo handling das requisições HTTP
- **Services**: Implementação da lógica de negócio
- **Repositories**: Camada de acesso a dados
- **DTOs**: Objetos de transferência de dados
- **Models**: Entidades do domínio

## 🔧 Configuração do Ambiente

1. Clone o repositório
2. Certifique-se de ter o .NET 9.0 SDK instalado
3. Configure a string de conexão do PostgreSQL no `appsettings.json`
4. Execute as migrações do Entity Framework:

A API estará disponível em `https://localhost:5001` e o Swagger UI em `https://localhost:5001/swagger`

## 📚 Documentação da API

A documentação completa da API está disponível através do Swagger UI quando o projeto está em execução no ambiente de
desenvolvimento.
