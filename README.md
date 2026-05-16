# 🍔 Good Hamburger

API REST + Frontend (Blazor WASM) para gerenciamento de pedidos de uma lanchonete.

---

## 🔐 Demo / Test Access

A aplicação possui usuários de demonstração para testes de fluxo:

### 👤 Admin
- Email: admin@goodhamburger.com
- Senha: 123456

⚠️ Essas contas são apenas para fins de demonstração e podem ser alteradas ou removidas a qualquer momento.

## 🎯 Contexto do Projeto

Sistema de pedidos com aplicação de regras de promoção baseadas em combinações de itens.

O projeto foi desenvolvido com foco em boas práticas de arquitetura (Clean Architecture), separação de responsabilidades e centralização das regras de negócio no domínio.

Além disso, inclui frontend em Blazor WASM e API REST documentada com Swagger.
---

## 🧱 Arquitetura

```
/src
  /GoodHamburger.WebAPI        # API ASP.NET Core
  /GoodHamburger.Application   # Casos de uso
  /GoodHamburger.Domain        # Regras de negócio
  /GoodHamburger.Infra         # Acesso a dados
  /GoodHamburger.Shared        # DTOs
  /GoodHamburger.BlazorWasm    # Frontend
/tests
```

---

## 🐳 Executar com Docker (RECOMENDADO)

### Pré-requisitos

* Docker

### Subir tudo

```bash
docker compose up --build -d
```

### Acessos

* Frontend (Blazor): [http://localhost:5000](http://localhost:5000)
* API (Swagger): [http://localhost:8080/swagger](http://localhost:8080/swagger)

> Observação: a API conecta no banco via `Server=db` (nome do serviço no compose).
---

## 🗄️ Inicialização do Banco de Dados

A aplicação aplica automaticamente as migrations do Entity Framework Core na inicialização da API. Esse comportamento está configurado no `Program.cs` da WebAPI, ao final do código, acima do app.Run();.

Esse mecanismo foi pensado principalmente para execução via Docker, onde a API pode iniciar antes do banco de dados estar totalmente disponível.

Nesses casos, o sistema realiza tentativas automáticas de rodar migrations, até que o banco esteja pronto.

Para execução local sem Docker, recomenda-se garantir que o banco de dados esteja disponível antes de iniciar a aplicação(Assim as migrations serão rodadas em cima). Caso a execução automática das migrations seja removida do `Program.cs`, elas podem ser aplicadas manualmente via CLI do Entity Framework Core.

## 📡 Endpoints

### 🧾 Pedidos

* `POST /api/Pedidos` → criar pedido
* `GET /api/Pedidos` → listar
* `GET /api/Pedidos/{id}` → obter por id
* `PUT /api/Pedidos/{id}` → atualizar
* `DELETE /api/Pedidos/{id}` → remover

### 📖 Cardápio

* `GET /api/Cardapio`

### 💸 Promoções

* `GET /api/Promocao/PromocoesAtivas`
* `GET /api/Promocao`
* `GET /api/Promocao/{id}`
* `PATCH /api/Promocao/{id}/alternar-status`

---

## 🧠 Regras de Negócio

* Sanduíche + Batata + Refrigerante → **20%**
* Sanduíche + Refrigerante → **15%**
* Sanduíche + Batata → **10%**

Restrições:

* Máx. 1 item por tipo (sanduíche, batata, refrigerante)
* Itens duplicados retornam erro

---

## 📦 Exemplos

### Criar pedido

```http
POST /api/Pedidos
Content-Type: application/json

{
  "itensIds": [
    "GUID_DO_SANDUICHE",
    "GUID_DA_BATATA",
    "GUID_DO_REFRIGERANTE"
  ]
}
```

### Resposta (resumo)

```json
{
  "id": "GUID",
  "subtotal": 9.5,
  "descontoPercentual": 0.2,
  "valorDesconto": 1.9,
  "totalFinal": 7.6
}
```

### Erro (exemplo)

```json
{
  "title": "Item duplicado",
  "status": 400,
  "detail": "Já existe um item deste tipo no pedido."
}
```

---

## 🧪 Testes

```bash
dotnet test
```

---

## ⚙️ Rodar sem Docker

```bash
dotnet restore
dotnet build
dotnet run --project src/GoodHamburger.WebAPI
```

---

## 🧠 Decisões Técnicas

* Separação em camadas (Clean Architecture)
* Regras de negócio centralizadas e reutilizáveis
* DTOs para desacoplamento
* Swagger para documentação
* Docker para execução simples

---

## 🔧 Possíveis Melhorias

* Fortalecer encapsulamento no domínio(Criar serviço de domínio)
* Autenticação (JWT)

---

## 👨‍💻 Autor

Esdras Lima
[LinkedIn](https://www.linkedin.com/in/esdrasdev/)
