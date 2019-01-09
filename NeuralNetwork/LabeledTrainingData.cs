namespace NeuralNetwork
{
	public class LabeledTrainingData
	{
		public byte[] Data { get; }
		public float[] Label { get; }

		public LabeledTrainingData(byte[] data, float[] label)
		{
			Data = new byte[data.Length];
			for (var i = 0; i < data.Length; i++)
			{
				Data[i] = data[i];
			}
			Label = label;
		}
	}
}