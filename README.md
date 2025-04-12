# 📰 Notícias com Vies Neutro - MVP

Um MVP simples que resume notícias brasileiras usando a API da OpenAI.  
Atualmente suporta apenas **G1 (g1.globo.com)**.

## ✅ Funcionalidades

- Resume artigos de notícias com viés neutro, reduzindo interpretações ideológicas
- Utiliza modelo da OpenAI (3.5 Turbo) via API (ChatGPT)
- Backend desenvolvido com ASP.NET Core (.NET 8)
- Frontend em React
- Chave de API armazenada com segurança via **User Secrets** do .NET

## 🚀 Como rodar o projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org)
- Uma chave de API da OpenAI: [Obtenha aqui](https://platform.openai.com/account/api-keys)

---

### 🛠️ Configuração do Backend

```bash
cd backend/NeutralNewsAPI

# Restaurar dependências
dotnet restore

# Executar a aplicação
dotnet run
```

---

## 💡 Importante: adicione sua chave da OpenAI usando user-secrets:

```bash
dotnet user-secrets set "OpenAiService:ApiKey" "sua_api_key_aqui"
```

💻 Configuração do Frontend

```bash
cd frontend

# Instalar dependências
npm install

# Rodar o frontend
npm run dev
```

---

📁 Estrutura de Pastas

```bash
backend/
└── NeutralNewsAPI/
    ├── Controllers/
    ├── Interfaces/
    ├── Services/
    ├── Utils/

frontend/
├── src/
│   ├── components/
│   ├── lib/
```

---

🧠 Planos futuros

- Suporte a mais sites de notícias brasileiras (UOL, Estadão, Folha, etc.)
- Resumos com viés à esquerda e à direita para comparação
- Comparativo com os dois lados da notícia para o leitor formar sua opinião
- Versão como extensão para navegador

---

⚠️ Limitações atuais

- Suporte apenas para notícias do G1.
- O extrator para o site da UOL foi comentado e requer scraping com suporte a JavaScript.
- Projeto em fase inicial de MVP — não recomendado para produção.

---

🔐 Segurança

A chave da OpenAI não está no repositório público. Foi armazenada com segurança usando:
```bash
dotnet user-secrets
```
Se quiser usar para testes, você deve configurar sua própria chave localmente.
