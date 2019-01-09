namespace NeuralNetwork
{
	public class ByteToFloatConverter : IFloatInputConverter
	{
		public float Convert(object input)
		{
			var byteInput = (byte)input;
			return byteInput / 255f;
		}
	}
}