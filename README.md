# BetRpa Telegram Bot

O **BetRpa Telegram Bot** é um bot desenvolvido para monitorar mercados de apostas em sites de esportes e enviar notificações via Telegram quando mercados específicos estão disponíveis. Ele foi desenvolvido em C# utilizando .NET 8 e emprega uma série de tecnologias e ferramentas para funcionar eficientemente.

## Tecnologias Usadas

- **.NET 8**: Plataforma de desenvolvimento utilizada para criar e gerenciar o projeto.
- **C#**: Linguagem de programação principal utilizada para o desenvolvimento do bot.
- **Telegram.Bot**: Biblioteca para interação com a API do Telegram.
- **Selenium WebDriver**: Usado para automação de navegador para monitorar mercados em sites de apostas.
- **EasyAutomationFramework**: Framework para facilitar a automação de tarefas com o Selenium.
- **Configuration System (appsettings.json)**: Para armazenar configurações do bot, como o token do Telegram.

## Funcionalidades

- **Adicionar Links**: O bot permite que usuários adicionem links de jogos que desejam monitorar.
- **Monitoramento de Mercado**: O bot monitora os mercados de apostas em intervalos regulares (10 segundos) e verifica a presença de mercados específicos como "Cartões" e "Escanteios".
- **Notificações**: Quando um mercado específico é encontrado, o bot envia uma notificação para o chat do usuário no Telegram com o link do jogo.
- **Início de Monitoramento**: O monitoramento é iniciado com o comando `/start` e continuará até que o usuário o pare ou o bot seja desligado.

## Configuração

1. **Clone o Repositório**

   ```bash
   git clone https://github.com/username/repository.git
   cd repository
Comandos do Bot
/addlink [link] - Adiciona um link para monitoramento.
/start - Inicia o processo de monitoramento dos links adicionados.
Arquitetura
TelegramNotificationService: Classe responsável pela interação com a API do Telegram, gerenciando chat IDs e enviando mensagens.
AutomationBetProvider: Classe que realiza a automação de navegação para verificar mercados de apostas e enviar notificações quando mercados específicos são encontrados.
appsettings.json: Arquivo de configuração que armazena o token do Telegram e outras configurações.
Contribuições
Se você deseja contribuir com o projeto, por favor, siga as seguintes etapas:

Fork o repositório.
Crie uma branch para a sua feature (git checkout -b feature/nome-da-feature).
Faça commit das suas alterações (git commit -am 'Add new feature').
Envie para o repositório remoto (git push origin feature/nome-da-feature).
Abra um Pull Request.


### Instruções Adicionais

- **Atualize os Detalhes**: Certifique-se de substituir `https://github.com/username/repository.git` pelo URL real do seu repositório e `SEU_TOKEN_AQUI` pelo seu token do Telegram.
- **Token de API**: Nunca coloque tokens sensíveis diretamente no README ou no repositório. Use exemplos genéricos e mantenha o token real em um local seguro e fora do controle de versão.
- **Licença**: Inclua um arquivo LICENSE no seu repositório para esclarecer a licença do projeto, se necessário.

Esse README deve ajudar a fornecer uma visão clara e abrangente sobre como configurar e usar seu bot. Se precisar de mais detalhes ou ajustes, sinta-se à vontade para personalizar conforme necessário.

Contato
Se você tiver alguma dúvida ou sugestão, sinta-se à vontade para abrir uma issue ou entrar em contato diretamente.
