using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TradingBotWebApp.Models;

namespace TradingBotWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHttpClientFactory _clientFactory;

		public HomeController(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var financialData = await GetFinancialData();
			return View(financialData);
		}

		private async Task<List<FinancialInstrument>> GetFinancialData()
		{
			var client = _clientFactory.CreateClient();
			var response = await client.GetAsync("API_URL");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			var data = JsonConvert.DeserializeObject<List<FinancialInstrument>>(content);
			return data;
		}
	}
}
