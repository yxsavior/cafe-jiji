# ☕🐾 Café Jiji — Sistema de Gestão de Pedidos e Cozinha

## 1. Descrição do Projeto

O Café Jiji é um sistema web desenvolvido para automatizar o gerenciamento operacional de uma cafeteria temática com espaço de interação e adoção de gatos.

O sistema foi criado para substituir o controle manual realizado em papel, reduzindo falhas de comunicação entre atendimento, cozinha e caixa, além de evitar perdas financeiras causadas por pedidos esquecidos ou lançados incorretamente.

A aplicação possui:

* gerenciamento de comandas por mesa;
* controle de pedidos em tempo real;
* painel de cozinha;
* controle de estoque e insumos;
* autenticação com níveis de acesso;
* dashboard gerencial.

O sistema foi desenvolvido com foco em responsividade e utilização em dispositivos móveis, permitindo que atendentes realizem lançamentos diretamente pelo celular.

> ⚠️ Observação: os endpoints relacionados ao módulo de gatos foram desenvolvidos na API, porém a funcionalidade completa ainda não foi integrada ao frontend do sistema.

---

# 2. Tecnologias Utilizadas

## Linguagem

* C# 13
* .NET 10

## Frameworks e Bibliotecas

* ASP.NET Core Web API
* Entity Framework Core
* Bootstrap 5
* BCrypt.Net
* Swagger / Swashbuckle
* DotNetEnv

## Banco de Dados

* MySQL
* Pomelo.EntityFrameworkCore.MySql

## Segurança

* JWT (JSON Web Token)
* Autenticação Bearer Token
* Controle de acesso por Roles
* Hash de senhas com BCrypt

## Documentação da API

* Swagger / OpenAPI

---

# 3. Instruções de Execução

Para rodar o projeto localmente siga os passos abaixo:

## 1. Clonar o repositório

```bash
git clone git@github.com:yxsavior/cafe-jiji.git
```

---

## 2. Acessar a pasta do projeto

```bash
cd cafejiji
```

---

## 3. Criar o arquivo `.env`

Crie um arquivo chamado `.env` na raiz do projeto usando como base o arquivo `.env.example`.

Exemplo:

```env
# BANCO
ConnectionStrings__DefaultConnection=Server=localhost;Database=CafeJijiDb;Uid=root;Pwd=SUA_SENHA;

# JWT
Jwt__Key=uma-chave-bem-grande-e-secreta-aqui-123456
Jwt__Issuer=CafeJijiAPI
Jwt__Audience=CafeJijiClient

# SEED USERS
SeedUsers__SrPassword=1234
SeedUsers__MidPassword=1234
SeedUsers__JrPassword=1234
```

---

## 4. Restaurar os pacotes

```bash
dotnet restore
```

---

## 5. Executar as migrations

```bash
dotnet ef database update
```

---

## 6. Executar o projeto

```bash
dotnet run
```

---

## 7. Acessar a documentação Swagger

Após iniciar a aplicação, acessar:

```bash
https://localhost:xxxx/swagger
```

---

# 4. Endpoints da API

Abaixo estão os principais endpoints disponíveis no sistema.

---

# 🔐 Auth

## Login

```http
POST /api/Auth/login
```

Responsável pela autenticação do usuário e geração do token JWT.

---

# 🐱 Gatos

> ⚠️ Endpoints disponíveis apenas na API. O módulo ainda não possui integração completa com o frontend.

## Listar gatos

```http
GET /api/gatos
```

## Cadastrar gato

```http
POST /api/gatos
```

## Buscar gato por ID

```http
GET /api/gatos/{id}
```

## Atualizar gato

```http
PUT /api/gatos/{id}
```

## Reservar gato

```http
PUT /api/gatos/{id}/reservar
```

## Registrar adoção

```http
PUT /api/gatos/{id}/adotado
```

---

# 📦 Insumos

## Listar insumos

```http
GET /api/Insumos
```

## Cadastrar insumo

```http
POST /api/Insumos
```

## Buscar insumo por ID

```http
GET /api/Insumos/{id}
```

## Atualizar insumo

```http
PUT /api/Insumos/{id}
```

## Remover insumo

```http
DELETE /api/Insumos/{id}
```

## Adicionar estoque

```http
PATCH /api/Insumos/{id}/adicionar-estoque
```

---

# ☕ Pedidos

## Criar pedido

```http
POST /api/pedidos
```

## Adicionar itens em lote

```http
POST /api/pedidos/{pedidoId}/itens/lote
```

## Listar pedidos da cozinha

```http
GET /api/pedidos/cozinha
```

## Atualizar status do item

```http
PUT /api/pedidos/itens/{idItem}/status
```

## Listar pedidos ativos

```http
GET /api/pedidos/ativos
```

## Finalizar pedido

```http
PUT /api/pedidos/{pedidoId}/finalizar
```

## Dashboard de pedidos

```http
GET /api/pedidos/dashboard
```

---

# 🍰 Produtos

## Listar produtos

```http
GET /api/produtos
```

## Cadastrar produto

```http
POST /api/produtos
```

## Atualizar produto

```http
PUT /api/produtos/{id}
```

## Remover produto

```http
DELETE /api/produtos/{id}
```

---

# 📄 Swagger

Documentação disponível em:

```bash
/swagger
```
