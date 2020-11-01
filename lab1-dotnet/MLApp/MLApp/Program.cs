using System;
using Microsoft.ML;
using MLAppML.Model;

namespace MLApp
{
    internal class Program
    {
        private const string ModelPath = @"C:\Users\Artem\AppData\Local\Temp\MLVSTools\MLAppML\MLAppML.Model\MLModel.zip";

        static void Main(string[] args)
        {
            MLContext mlContext = new MLContext();

            // Load model
            ITransformer mlModel = mlContext.Model.Load(ModelPath, out _);

            PredictionEngine<ModelInput, ModelOutput> predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            ModelInput modelInput = new ModelInput { SentimentText = "HIGHLIGHTS VIDEO ON YOUTUBE" };

            ModelOutput modelOutput = predEngine.Predict(modelInput);

            Console.WriteLine(modelOutput.Prediction);
        }
    }
}