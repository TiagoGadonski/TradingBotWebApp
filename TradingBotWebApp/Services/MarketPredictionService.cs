using Microsoft.ML;
using TradingBotWebApp.Models;

namespace TradingBotWebApp.Services
{
	public class MarketPredictionService
	{
		private readonly MLContext _mlContext;
		private ITransformer _model;

		public MarketPredictionService()
		{
			_mlContext = new MLContext();
		}

		public void TrainModel(IEnumerable<MarketData> data)
		{
			IDataView dataView = _mlContext.Data.LoadFromEnumerable(data);

			var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(MarketData.LastPrice), nameof(MarketData.ChangePercent))
				.Append(_mlContext.BinaryClassification.Trainers.FastTree());

			_model = pipeline.Fit(dataView);
		}

		public bool Predict(MarketData newData)
		{
			var predictor = _mlContext.Model.CreatePredictionEngine<MarketData, MarketPrediction>(_model);
			var prediction = predictor.Predict(newData);
			return prediction.WillIncrease;
		}
	}
}
