using System;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        public float[] Outputs { get; }

        public readonly NeuralNetworkInputLayer InputLayer;
        public readonly HiddenNetworkLayer[] HiddenNetworkLayers;

        public NeuralNetwork(NeuralNetworkInputLayer inputLayer, HiddenNetworkLayer[] hiddenLayers)
        {
            if (hiddenLayers.Length < 1) throw new ArgumentException("There must be a minimum of 1 hidden layer");

            InputLayer = inputLayer;
            HiddenNetworkLayers = hiddenLayers;
            Outputs = hiddenLayers[hiddenLayers.Length - 1].Outputs;
            HiddenNetworkLayers[0].ConnectLayer(InputLayer.Outputs);
            for (var i = 1; i < HiddenNetworkLayers.Length; i++)
            {
                var currentLayer = HiddenNetworkLayers[i];
                var previousLayer = HiddenNetworkLayers[i - 1];
                currentLayer.ConnectLayer(previousLayer.Outputs);
            }
        }

        public void UpdateOutputs()
        {
			//First update the input layer and then update all layers till we reached the output layer
            InputLayer.UpdateOutputs();
            foreach (var hiddenNetworkLayer in HiddenNetworkLayers)
            {
                hiddenNetworkLayer.UpdateOutputs();
            }
        }
    }
}