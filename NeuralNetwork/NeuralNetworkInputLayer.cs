namespace NeuralNetwork
{
	public class NeuralNetworkInputLayer
	{
		public object[] Inputs { get; }
		private IFloatInputConverter[] FloatInputConverters { get; }
		public float[] Outputs { get; }

		public NeuralNetworkInputLayer(IFloatInputConverter[] floatInputConverters)
		{
			FloatInputConverters = floatInputConverters;
			Outputs = new float[floatInputConverters.Length];
			Inputs = new object[floatInputConverters.Length];
		}

		public void UpdateOutputs()
		{
			for (var i = 0; i < FloatInputConverters.Length; i++)
			{
				Outputs[i] = FloatInputConverters[i].Convert(Inputs[i]);
			}
		}
	}
}