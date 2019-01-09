using System;

namespace NeuralNetwork
{
    public class BackPropagationTrainer
    {
        private readonly LabeledTrainingData[] _trainingSets;

        public BackPropagationTrainer(LabeledTrainingData[] trainingSets)
        {
            _trainingSets = trainingSets;
        }

        public void TrainNetwork(NeuralNetwork neuralNetwork, float learningRate)
        {
            AssignRandomWeights(neuralNetwork, learningRate * 0.1f);
            foreach (var trainingSet in _trainingSets)
            {
                
                for (var i = 0; i < neuralNetwork.InputLayer.Inputs.Length; i++)
                {
                    neuralNetwork.InputLayer.Inputs[i] = trainingSet.Data[i];
                }
                neuralNetwork.UpdateOutputs();
                var errors = CalculateOutputErrors(neuralNetwork, trainingSet);
                //neuralNetwork.Outputs
                for (var i = neuralNetwork.HiddenNetworkLayers.Length - 1; i >= 0; i--)
                {
                    var networkLayer = neuralNetwork.HiddenNetworkLayers[i];
                    ModifyWeights(networkLayer, learningRate, errors);
                    errors = PropagateErrors(networkLayer, errors);
                }
            }
        }

        public void AssignRandomWeights(NeuralNetwork neuralNetwork, float percentage)
        {
	        var randomNumberGenerator = new Random();
			foreach (var neuralNetworkHiddenNetworkLayer in neuralNetwork.HiddenNetworkLayers)
            {
                var totalWeights = neuralNetworkHiddenNetworkLayer.Weights.Length;
                var amountOfRandomWeights = (int)(totalWeights * percentage);


                for (int i = 0; i < amountOfRandomWeights; i++)
                {
                    var randomWeight = randomNumberGenerator.Next(0, 1000) / 1000f;
                    var weightIndex = randomNumberGenerator.Next(0, neuralNetworkHiddenNetworkLayer.Weights.Length);
                    neuralNetworkHiddenNetworkLayer.Weights[weightIndex] = randomWeight;
                }
            }
        }

        private float[] CalculateOutputErrors(NeuralNetwork neuralNetwork, LabeledTrainingData trainingSet)
        {
            var errors = new float[neuralNetwork.Outputs.Length];
            for (var j = 0; j < neuralNetwork.Outputs.Length; j++)
            {
                var output = neuralNetwork.Outputs[j];
                errors[j] = output * (1 - output) * (trainingSet.Label[j] - output);
            }
            return errors;
        }

        private float[] PropagateErrors(HiddenNetworkLayer networkLayer, float[] errors)
        {
            var propagatedErrors = new float[networkLayer.Inputs.Length];

            for (int j = 0; j < networkLayer.Outputs.Length; j++)
            {
                var output = networkLayer.Outputs[j];
                //Update the expected output for the next network layer
                for (var k = 0; k < propagatedErrors.Length; k++)
                {
                    propagatedErrors[k] += networkLayer.Weights[k] * errors[j] * output * (1 - output);
                }
            }
            return propagatedErrors;
        }

        private void ModifyWeights(HiddenNetworkLayer networkLayer, float learningRate, float[] errors)
        {
            var weightIndex = 0;

            for (var j = 0; j < networkLayer.Outputs.Length; j++)
            {
                //Update the weights
                for (var k = 0; k < networkLayer.Inputs.Length; k++)
                {
                    networkLayer.Weights[weightIndex] = networkLayer.Weights[weightIndex] + errors[j] * networkLayer.Inputs[k] * learningRate;
                    weightIndex++;
                }
            }
        }
    }
}