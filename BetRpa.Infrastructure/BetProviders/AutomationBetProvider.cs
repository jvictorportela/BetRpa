using BetRpa.Infrastructure.Notifications;
using EasyAutomationFramework;

namespace BetRpa.Infrastructure.BetProviders;

public class AutomationBetProvider : Web
{
    private readonly TelegramNotificationService _notificationService;

    public AutomationBetProvider(TelegramNotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void GetMarkets(List<string> linksApostas)
    {
        try
        {
            var jogosParaRemover = new List<string>();
            var mercadoCartoes = "Cartões";
            var mercadoEscanteios = "Escanteios";

            if (driver == null)
            {
                // Cria uma cópia da lista para iteração
                var linksApostasParaIterar = new List<string>(linksApostas);

                for (int i = 0; i < linksApostasParaIterar.Count; i++)
                {
                    var linkAposta = linksApostasParaIterar[i];

                    StartBrowser();
                    Navigate(linkAposta);
                    WaitForLoad();

                    bool encontrouCartoes = false;
                    bool encontrouEscanteios = false;

                    var cartoes = GetValue(TypeElement.Xpath, "//*[contains(text(), 'Cartões')]");
                    var escanteios = GetValue(TypeElement.Xpath, "//*[contains(text(), 'Escanteios')]");

                    if (cartoes.Sucesso && cartoes.Value.Equals(mercadoCartoes))
                    {
                        encontrouCartoes = true;
                        _notificationService.SendNotificationAsync($"Mercado 'Cartões' encontrado! {linkAposta}").Wait(); // Enviar notificação
                    }

                    if (escanteios.Sucesso && escanteios.Value.Equals(mercadoEscanteios))
                    {
                        encontrouEscanteios = true;
                        _notificationService.SendNotificationAsync($"Mercado 'Escanteios' encontrado! {linkAposta}").Wait(); // Enviar notificação
                    }

                    if (encontrouCartoes && encontrouEscanteios)
                    {
                        jogosParaRemover.Add(linkAposta);
                    }

                    CloseBrowser();
                }

                // Remove os itens da lista original após a iteração
                foreach (var jogo in jogosParaRemover)
                {
                    linksApostas.Remove(jogo);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
