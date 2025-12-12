using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TerminalLibrary.Models
{
    

        public class WebScrappingService
        {
            private readonly HttpClient _httpClient;
            private readonly ILogger<WebScrappingService> _logger;

            public WebScrappingService(HttpClient httpClient, ILogger<WebScrappingService> logger)
            {
                _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<string> GetEuroToLekExchangeRateAsync()
            {
                try
                {
                    var url = "https://www.bankofalbania.org/Markets/Official_exchange_rate/";
                    var html = await _httpClient.GetStringAsync(url);
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);

                    _logger.LogInformation(htmlDocument.DocumentNode.OuterHtml);

                    var euroElement = htmlDocument.DocumentNode.SelectSingleNode("//td[text()='Euro']/following-sibling::td[2]\r\n");
                    if (euroElement != null)
                    {
                        var euro = euroElement.InnerText.Trim();
                        _logger.LogInformation("Euro: " + euro);
                        return euro;
                    }
                    else
                    {
                        _logger.LogError("Euro element not found. HTML content may have changed.");
                        _logger.LogInformation(htmlDocument.DocumentNode.InnerHtml);
                        throw new Exception("Euro element not found.");
                    }
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError($"HTTP request failed: {ex.Message}");
                    throw new Exception("Request Failed");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred: {ex.Message}");
                    throw new Exception("Error occurred", ex);
                }
            }

        }
    

}
