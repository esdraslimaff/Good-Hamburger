# 🍔 Good Hamburger

API REST + Frontend (Blazor WASM) para gerenciamento de pedidos de uma lanchonete.

---

## 🚀 Objetivo

* Registrar pedidos
* Aplicar regras de desconto automaticamente
* Expor API REST documentada

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
docker compose up --build
```

### Acessos

* Frontend (Blazor): [http://localhost:5000](http://localhost:5000)
* API (Swagger): [http://localhost:8080/swagger](http://localhost:8080/swagger)

> Observação: a API conecta no banco via `Server=db` (nome do serviço no compose).

---

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
    "UUID_DO_SANDUICHE",
    "UUID_DA_BATATA",
    "UUID_DO_REFRIGERANTE"
  ]
}
```

### Resposta (resumo)

```json
{
  "id": "uuid",
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
