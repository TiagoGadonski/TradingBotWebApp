using Newtonsoft.Json;
using TradingBotWebApp.Models;

namespace TradingBotWebApp.Services
{
	public class MarketDataService
	{
		private readonly IHttpClientFactory _clientFactory;

		public MarketDataService(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public async Task<IEnumerable<FinancialInstrument>> GetMarketDataAsync()
		{
			// Aqui, você faria chamadas para APIs distintas para ações e criptomoedas
			var client = _clientFactory.CreateClient();
			var cryptoResponse = await client.GetAsync("CRYPTO_API_URL");
			var stockResponse = await client.GetAsync("STOCK_API_URL");
			await Task.WhenAll(cryptoResponse, stockResponse);
			cryptoResponse.EnsureSuccessStatusCode();
			stockResponse.EnsureSuccessStatusCode();

			var cryptoContent = await cryptoResponse.Content.ReadAsStringAsync();
			var stockContent = await stockResponse.Content.ReadAsStringAsync();

			var cryptoData = JsonConvert.DeserializeObject<List<FinancialInstrument>>(cryptoContent);
			var stockData = JsonConvert.DeserializeObject<List<FinancialInstrument>>(stockContent);

			return cryptoData.Concat(stockData);
		}

		// Você poderia adicionar métodos para filtrar os dados aqui
	}
}
