namespace NeuralNetwork
{
	public class FloatToFloatConverter : IFloatInputConverter
	{
		public float Convert(object input)
		{
			return (float)input;
		}
	}
}