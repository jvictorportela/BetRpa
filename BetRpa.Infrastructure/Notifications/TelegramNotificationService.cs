using BetRpa.Infrastructure.BetProviders;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BetRpa.Infrastructure.Notifications;

public class TelegramNotificationService
{
    private readonly TelegramBotClient _botClient;
    private readonly HashSet<long> _chatIds = new HashSet<long>(); // Armazena o chat ID
    private readonly Dictionary<long, List<string>> _userLinks = new Dictionary<long, List<string>>(); // Armazena links por usuário
    private Timer _monitoringTimer; // Timer para monitoramento
    private readonly object _timerLock = new object(); // Para sincronizar o acesso ao timer

    public TelegramNotificationService(string token)
    {
        _botClient = new TelegramBotClient(token);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            cancellationToken: cancellationToken
        );

        // Inicia o timer para monitorar links a cada 10 segundos
        _monitoringTimer = new Timer(MonitorLinks, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message != null && update.Message.Type == MessageType.Text)
        {
            var message = update.Message;
            var messageText = message.Text;
            var chatId = message.Chat.Id;

            // Adiciona o chat ID se não estiver presente
            if (!_chatIds.Contains(chatId))
            {
                _chatIds.Add(chatId);
                _userLinks[chatId] = new List<string>(); // Inicializa a lista de links para o usuário
            }

            // Processa a mensagem do usuário
            if (messageText.StartsWith("/addlink"))
            {
                var link = messageText.Replace("/addlink ", string.Empty);
                if (_userLinks.ContainsKey(chatId))
                {
                    _userLinks[chatId].Add(link);
                    Console.WriteLine($"Link adicionado: {link}");
                    await botClient.SendTextMessageAsync(chatId, $"Link adicionado");
                }
            }
            if (messageText.StartsWith("/start"))
            {
                await botClient.SendTextMessageAsync(chatId, "Monitoramento iniciado. O bot irá verificar os links a cada 10 segundos.");
            }
        }
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Polling error: {exception.Message}");
        return Task.CompletedTask;
    }

    private void MonitorLinks(object state)
    {
        lock (_timerLock) // Garantir que o acesso ao timer é thread-safe
        {
            Console.WriteLine("Início do ciclo de monitoramento.");

            foreach (var chatId in _chatIds)
            {
                if (_userLinks.TryGetValue(chatId, out var links) && links.Count > 0)
                {
                    var bot = new AutomationBetProvider(this); // Instanciar o seu provedor de apostas
                    bot.GetMarkets(links); // Executa a automação

                    Console.WriteLine("Ciclo de monitoramento finalizado.");
                }
                else
                {
                    Console.WriteLine("Nenhum link para monitorar.");
                }
            }
        }
    }

    public async Task SendNotificationAsync(string message)
    {
        foreach (var chatId in _chatIds)
        {
            await _botClient.SendTextMessageAsync(chatId, message);
        }
    }
}