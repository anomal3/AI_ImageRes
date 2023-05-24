﻿
// This file was auto-generated by ML.NET Model Builder. 

using MLConsole;
using System.IO;

// Create single instance of sample data from first line of dataset for model input
var imageBytes = File.ReadAllBytes(@"D:\ML\Car\image0.jpg");
EnviromentModel.ModelInput sampleData = new EnviromentModel.ModelInput()
{
    ImageSource = imageBytes,
};

// Make a single prediction on the sample data and print results.
var predictionResult = EnviromentModel.Predict(sampleData);
Console.WriteLine($"\n\nPredicted Label value: {predictionResult.PredictedLabel} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n\n");