# Integração com iDFlex - Control iD 🔐

Projeto ASP.NET Core para integração com o equipamento **iDFlex** da **Control iD**, utilizando a API REST do dispositivo.

## Funcionalidades

- Login com autenticação via API (`/login.fcgi`)
- Abertura remota da SecBox (`/execute_actions.fcgi`)
- Logout (finaliza a sessão atual)
- Dashboard inicial com controle via botões

## Tecnologias utilizadas

- C# com ASP.NET Core MVC
- HTML + Razor Pages
- API REST do iDFlex
- HttpClient / JSON Serialization
- Session para controle de autenticação

## Como rodar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/cmathxus/idflex-integration.git
   ```

2. Acesse a pasta do projeto:
   ```bash
   cd idflex-integration
   ```

3. Instale as dependências e rode:
   ```bash
   dotnet run
   ```

4. Acesse no navegador:
   ```
   http://localhost:5000
   ```

## Observações

- O IP do equipamento iDFlex precisa estar acessível pela rede local.
- As sessões são armazenadas usando `HttpContext.Session`.
- A cada login bem-sucedido, é gerado um novo token de sessão que é usado nas requisições seguintes.

## Próximos passos

- [ ] Cadastro e listagem de usuários
- [ ] Controle de permissões
- [ ] Interface responsiva
- [ ] Logs de ações executadas

---
