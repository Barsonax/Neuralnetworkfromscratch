using System;

namespace NeuralNetwork
{
    public class HiddenNetworkLayer
    {
        public float[] Outputs { get; private set; }
        public float[] Weights { get; private set; }
        public float[] Inputs { get; private set; }

        public HiddenNetworkLayer(int amountOfNeurons)
        {
            Outputs = new float[amountOfNeurons];
        }

        public void ConnectLayer(float[] inputs)
        {
            Inputs = inputs;
            Weights = new float[inputs.Length * Outputs.Length];
        }

        public void UpdateOutputs()
        {
            var weightIndex = 0;
            for (var j = 0; j < Outputs.Length; j++)
            {
                var totalInput = 0f;
                foreach (var input in Inputs)
                {
                    totalInput += Weights[weightIndex] * input;
                    weightIndex++;
                }
                Outputs[j] = 1f / (1f + (float)Math.Pow(Math.E, -totalInput));
             }
        }
    }
}