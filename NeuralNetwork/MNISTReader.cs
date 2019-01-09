using System;
using System.IO;
using System.Linq;

namespace NeuralNetwork
{
	/// <summary>
	/// Class for reading the MNIST dataset which can be found here: http://yann.lecun.com/exdb/mnist/
	/// </summary>
	public class MNISTReader
	{
		private const int OFFSET_SIZE = 4;

		private const int NUMBER_ITEMS_OFFSET = 4;
		private const int ITEMS_SIZE = 4;

		public const int ROWS = 28;

		public const int COLUMNS = 28;

		private const int IMAGE_OFFSET = 16;
		private const int IMAGE_SIZE = ROWS * COLUMNS;

		/// <summary>
		/// Reads the labels and images from the MNIST dataset and puts them in <see cref="LabeledTrainingData"/>
		/// </summary>
		public LabeledTrainingData[] LoadDigitImages(byte[] labelBytes, byte[] imageBytes)
		{

			var numberOfLabels = BitConverter.ToInt32(labelBytes.CopyRange(NUMBER_ITEMS_OFFSET, 4).Reverse().ToArray(), 0);
			var numberOfImages = BitConverter.ToInt32(imageBytes.CopyRange(NUMBER_ITEMS_OFFSET, 4).Reverse().ToArray(), 0);

			if (numberOfImages != numberOfLabels)
			{
				throw new IOException("The number of labels and images do not match!");
			}

			var images = new LabeledTrainingData[numberOfLabels];
			for (var i = 0; i < numberOfLabels; i++)
			{
				var label = labelBytes[OFFSET_SIZE + ITEMS_SIZE + i];
				var labels = new float[10];
				labels[label] = 1;
				var imageData = imageBytes.CopyRange(i * IMAGE_SIZE + IMAGE_OFFSET, IMAGE_SIZE);
				images[i] = new LabeledTrainingData(imageData, labels);
			}
			return images;
		}
	}
}