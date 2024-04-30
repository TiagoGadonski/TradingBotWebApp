namespace TradingBotWebApp.Models
{
	public class FinancialInstrument
	{
		public string Symbol { get; set; }
		public double LastPrice { get; set; }
		public double ChangePercent { get; set; }
		public double Volume { get; set; }
		public bool IsCrypto { get; set; }
	}
}
