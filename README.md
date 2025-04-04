# 🏢 Agenda Room API - Desafio Técnico

Seja bem-vindo(a) ao **Agenda Room API**, uma aplicação RESTful desenvolvida com **.NET 8** para o desafio técnico de cadastro e gerenciamento de **salas de reunião**. 

O projeto contempla autenticação segura via **JWT**, **CRUD completo** de usuários, salas e reservas, além de validações inteligentes para evitar conflitos de horário nas reservas.

---

## 🚀 Tecnologias Utilizadas

- ✅ **.NET 8** — Framework principal da aplicação
- ✅ **Entity Framework Core** — ORM para acesso ao banco de dados
- ✅ **PostgreSQL** — Banco de dados relacional
- ✅ **JWT (Json Web Token)** — Autenticação segura e moderna
- ✅ **Swagger (Swashbuckle)** — Documentação e testes da API
- ✅ **AutoMapper** — Mapeamento de objetos (DTOs ↔ Entidades)
- ✅ **Docker + Docker Compose** — Containerização da aplicação

---

## 📦 Como Executar Localmente

### ⚙️ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker + Docker Compose](https://www.docker.com/)
- [Git](https://git-scm.com/)

### 📥 Clonando o Repositório

```bash
git clone https://github.com/italodev77/agenda_room_desafio.git
cd agenda_room_desafio

🐳 Subindo com Docker Compose

docker-compose up --build

🔗 Acesse o Swagger em: http://localhost:5000/swagger

📁 Estrutura do Projeto
bash
Copiar
Editar
src/
├── AgendaRoom.Api              # Camada principal da API (Controllers, Program.cs, autenticação)
├── AgendaRoom.Application      
├── AgendaRoom.Domain           # Entidades, interfaces e enums
├── AgendaRoom.Infra            # Repositórios e camada de acesso a dados
├── AgendaRoom.Infra.Context    # DbContext, configurações do EF e Migrations
🧩 Funcionalidades
👥 Usuários
✅ Criar usuário

✅ Editar usuário

✅ Excluir usuário

✅ Login com autenticação via e-mail/senha

✅ Geração e uso de JWT Token

Cada usuário possui: Nome, Email, Senha (criptografada)

🏛️ Salas
✅ Criar sala

✅ Editar sala

✅ Excluir sala

Cada sala possui: Nome, Capacidade máxima de pessoas

📆 Reservas
✅ Criar nova reserva

✅ Cancelar reserva existente

✅ Listar reservas por usuário

✅ Listar reservas por sala

Validações importantes:

❌ Não permite reservas sobrepostas na mesma sala

📅 As reservas devem começar e terminar no mesmo dia

🔐 Autenticação com JWT
Após o login, o usuário receberá um token JWT. Esse token deve ser adicionado no cabeçalho de cada requisição autenticada:

Authorization: Bearer {seu_token}

🛠️ Documentação com Swagger
A API está documentada e testável via Swagger UI.

📎 Acesse: http://localhost:5000/swagger

🐘 Banco de Dados
PostgreSQL

As tabelas são criadas automaticamente com o uso de Migrations

As configurações de conexão estão no docker-compose.yml

📂 Migrations: AgendaRoom.Infra.Context/Migrations

🐳 Docker Compose

version: '3.8'
services:
  postgres:
    image: postgres:17.4-alpine
    container_name: agendaroomdb
    environment:
      POSTGRES_DB: agendaroomdb
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
    ports:
      - "5432:5432"
    restart: always
 
✅ Requisitos do Desafio Atendidos
Funcionalidade	Status
Gerenciamento de usuários (CRUD)	✅
Autenticação com JWT	✅
Gerenciamento de salas (CRUD)	✅
Reservas com validação de conflito	✅
Listagem de reservas por usuário/sala	✅
👨‍💻 Autor
Desenvolvido com 💙 por Ítalo Dev

📄 Licença
Este projeto está licenciado sob a MIT License.
Sinta-se à vontade para usar, contribuir e compartilhar!
