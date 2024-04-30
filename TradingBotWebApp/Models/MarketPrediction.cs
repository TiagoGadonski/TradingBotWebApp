using Microsoft.ML.Data;

namespace TradingBotWebApp.Models
{
	public class MarketData
	{
		public float LastPrice { get; set; }
		public float ChangePercent { get; set; }
	}

	public class MarketPrediction
	{
		[ColumnName("PredictedLabel")]
		public bool WillIncrease { get; set; }
	}
}
