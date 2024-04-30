using TradingBotWebApp.Models;

namespace TradingBotWebApp.Services
{
	public class MarketDataBackgroundService : IHostedService, IDisposable
	{
		private Timer _timer;
		private readonly MarketDataService _marketDataService;

		public MarketDataBackgroundService(MarketDataService marketDataService)
		{
			_marketDataService = marketDataService;
		}

		public Task StartAsync(CancellationToken stoppingToken)
		{
			_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(30));
			return Task.CompletedTask;
		}

		private async void DoWork(object state)
		{
			var marketData = await _marketDataService.GetMarketDataAsync();
			foreach (var data in marketData)
			{
				bool willIncrease = _marketPredictionService.Predict(new MarketData
				{
					LastPrice = data.LastPrice,
					ChangePercent = data.ChangePercent
				});

				// Logica para tomar ações baseada na previsão
				if (willIncrease)
				{
					// Implementar ações recomendadas, e.g., comprar
				}
			}
		}

		public Task StopAsync(CancellationToken stoppingToken)
		{
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}


	}
}
