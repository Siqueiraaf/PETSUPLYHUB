# Documentação do Projeto que está em Andamento...

## Visão Geral
Este documento detalha a estrutura do projeto, explicando suas funcionalidades e componentes essenciais.

## Estrutura do Projeto

```
Contracts
├───DTOs
├───Validators
Controllers
├───Product
├───User
Data
Docs
Middleware
├───Authentication
├───ErrorHandling
Migrations
Models
Properties
Repositories
├───Implementations
├───Interfaces
Services
├───LoginAuth
├───Products
├───User
obj
└───Debug
    └───net9.0
        ├───ref
        ├───refint
        └───staticwebassets
```

### Descrição das Pastas

- **Contracts**: Contém contratos (DTOs) e validadores utilizados no projeto.
- **Controllers**: Responsáveis por lidar com as requisições HTTP e chamar os serviços correspondentes.
  - `Product`: Controlador de produtos.
  - `User`: Controlador de usuários.
- **Data**: Contém a configuração do `DbContext`, facilitando o acesso e a manipulação do banco de dados.
- **Docs**: Armazena documentação do projeto.
- **Middleware**: Implementa manipulação de autenticação e tratamento de erros globais.
  - `Authentication`: Middleware para autenticação.
  - `ErrorHandling`: Middleware para tratamento de erros.
- **Migrations**: Armazena as migrações para controle de versão do banco de dados.
- **Models**: Define as entidades do sistema.
- **Repositories**: Centraliza a lógica de acesso ao banco de dados, garantindo separação de preocupações.
  - `Implementations`: Implementação concreta dos repositórios.
  - `Interfaces`: Interfaces dos repositórios.
- **Services**: Contém a lógica de negócio da aplicação, sendo consumida pelos controllers.
  - `LoginAuth`: Serviços de autenticação e login.
  - `Products`: Serviços relacionados a produtos.
  - `User`: Serviços relacionados a usuários.
- **obj**: Pasta gerada automaticamente pelo .NET durante a compilação.
- **Properties**: Contém arquivos de configuração do projeto.

## Modelagem de Dados

O modelo utiliza **dois identificadores**:
- **ID Interno (int)**: Utilizado para otimizar buscas e operações dentro do banco de dados.
- **ID Externo (GUID)**: Exposto para interação com clientes e APIs, garantindo segurança.

## Configuração do DbContext

O `DbContext` gerencia o acesso ao banco de dados, permitindo operações CRUD de forma simplificada. Ele inclui:
- Configuração de conexão via `appsettings.json`
- Definição de `DbSet` para cada entidade
- Uso de migrações para controle de versão do esquema do banco

## Data Transfer Objects (DTOs)

Os DTOs foram criados utilizando **records**, proporcionando:
- **Imutabilidade**, garantindo segurança na manipulação de dados
- **Facilidade na comparação de objetos**, pois records possuem comparação baseada em valor

## Injeção de Dependência

O projeto utiliza **injeção de dependência** para modularidade e manutenção.
Os serviços e repositórios são registrados no `Program.cs`:
```csharp
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
```

## Repositório e Persistência

O repositório cuida do acesso ao banco de dados, separando essa parte da lógica de negócio. Ele usa `IEnumerable` para trabalhar com dados depois que são carregados na memória, o que dá mais flexibilidade para manipular os dados.

Embora `IEnumerable` não seja tão eficiente quanto `IQueryable` para otimizar consultas no banco de dados, ele é útil quando a lógica de negócio precisa tratar os dados após a consulta ou quando já se tem os dados na memória. Isso simplifica as operações, mas pode afetar a performance se houver muitos dados.

## Middleware para Exceções

Um middleware customizado foi criado para capturar exceções e retornar respostas padronizadas:
```csharp
app.UseMiddleware<ExceptionMiddleware>();
```
Isso melhora a manutenção do código e padroniza os retornos de erro.

## Validação com FluentValidation

A validação de entradas é feita com **FluentValidation**, aumentando a legibilidade do código.
Exemplo de validação de um DTO:
```csharp
public class UsuarioValidator : AbstractValidator<UsuarioDTO>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.Nome).NotEmpty().WithMessage("O nome é obrigatório");
        RuleFor(u => u.Email).EmailAddress().WithMessage("E-mail inválido");
    }
}
```

## Autenticação com JWT e Identity

O sistema utiliza **JWT (JSON Web Token)** para autenticação, integrando-se com **Identity**.
Inclui:
- **Geração de tokens** com expiração configurável
- **Uso de roles e claims** para controle de acesso
- **Refresh tokens** para manutenção de sessões seguras

Exemplo de geração do token:
```csharp
var token = new JwtSecurityTokenHandler().WriteToken(jwt);
```

