using BetRpa.Infrastructure.BetProviders;
using BetRpa.Infrastructure.Notifications;
using BetRpa.TelegramBot.Configuration;

var configuration = new TelegramConfiguration();
var token = configuration.Token;
var botService = new TelegramNotificationService(token);

// Inicializa o provedor de automação com o serviço de notificação
var bot = new AutomationBetProvider(botService);

var cancellationTokenSource = new CancellationTokenSource();

// Inicia o bot do Telegram
await botService.StartAsync(cancellationTokenSource.Token);

Console.WriteLine("Bot is running...");
Console.ReadLine();