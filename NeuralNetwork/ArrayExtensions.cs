namespace NeuralNetwork
{
	public static class ArrayExtensions
	{
		public static T[] CopyRange<T>(this T[] inputArray, int startIndex, int length)
		{
			var outputArray = new T[length];
			for (var i = startIndex; i < startIndex + length; i++)
			{
				outputArray[i - startIndex] = inputArray[i];
			}
			return outputArray;
		}
	}
}