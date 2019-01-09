using System;
using System.IO;
using System.Reflection;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            var labelData = ReadAllBytes(Assembly.GetExecutingAssembly().GetManifestResourceStream("NeuralNetwork.Resources.train-labels.idx1-ubyte"));
            var imageData = ReadAllBytes(Assembly.GetExecutingAssembly().GetManifestResourceStream("NeuralNetwork.Resources.train-images.idx3-ubyte"));

            var reader = new MNISTReader();
            var labeledTrainingDatas = reader.LoadDigitImages(labelData, imageData);


            var hiddenLayers = new[] { new HiddenNetworkLayer(10) };
            var converters = new IFloatInputConverter[784];
            for (var i = 0; i < converters.Length; i++)
            {
                converters[i] = new ByteToFloatConverter();
            }
            var inputLayer = new NeuralNetworkInputLayer(converters);
            var neuralNetwork = new NeuralNetwork(inputLayer, hiddenLayers);

            var trainer = new BackPropagationTrainer(labeledTrainingDatas);
            for (var i = 0; i < 10000; i++)
            {
                var learningRate = 1f / ((i + 1) * 100);
                trainer.TrainNetwork(neuralNetwork, learningRate);
                var correct = 0;
                var incorrect = 0;
                for (int j = 0; j < labeledTrainingDatas.Length; j++)
                {
                    for (var k = 0; k < neuralNetwork.InputLayer.Inputs.Length; k++)
                    {
                        neuralNetwork.InputLayer.Inputs[k] = labeledTrainingDatas[j].Data[k];
                    }
                    neuralNetwork.UpdateOutputs();
                    var maxOutputValue = 0f;
                    var index = 0;
                    for (int k = 0; k < neuralNetwork.Outputs.Length; k++)
                    {
                        var outputValue = neuralNetwork.Outputs[k];
                        if (outputValue > maxOutputValue)
                        {
                            maxOutputValue = outputValue;
                            index = k;
                        }
                    }

                    var expectedMaxOutputValue = 0f;
                    var expectedIndex = 0;
                    for (int k = 0; k < labeledTrainingDatas[j].Label.Length; k++)
                    {
                        var outputValue = labeledTrainingDatas[j].Label[k];
                        if (outputValue > expectedMaxOutputValue)
                        {
                            expectedMaxOutputValue = outputValue;
                            expectedIndex = k;
                        }
                    }
                    if (index == expectedIndex) correct++;
                    else incorrect++;
                }
                Console.WriteLine($"Itteration: {i}  LearningRate: {learningRate}  Correct: {correct}  Incorrect: {incorrect}");
            }
            Console.ReadKey();
        }

	    private static byte[] ReadAllBytes(Stream stream)
	    {
		    byte[] data;
		    using (var memoryStream = new MemoryStream())
		    {
			    stream.CopyTo(memoryStream);
			    data = memoryStream.ToArray();
		    }
		    return data;
	    }
    }
}
