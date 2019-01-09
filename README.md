# Neuralnetworkfromscratch
Shows how to create a neural network from scratch in C# without a 3th party library. I wrote this a long time ago purely for learning purposes.

## Le tour
The mnist dataset can be found in [NeuralNetwork/Resources](https://github.com/Barsonax/Neuralnetworkfromscratch/tree/master/NeuralNetwork/Resources). The [MNISTReader](https://github.com/Barsonax/Neuralnetworkfromscratch/blob/master/NeuralNetwork/MNISTReader.cs) class is responsible for parsing this into a more useful/readable datastructure. This dataset contains both a training dataset and a separate dataset that is used for validating the correctness of the network.

The actual training of the network is done using the training data set with the [BackPropagationTrainer](https://github.com/Barsonax/Neuralnetworkfromscratch/blob/master/NeuralNetwork/BackPropagationTrainer.cs) class.

After training it will validate the network using the validation dataset and print these results to the console.

This gets repeated until you decide to stop it.

Be sure to choose `Release` and not `Debug` as build configuration when you actually want to train the network and not Debug it. There is a big difference in speed and nobody likes waiting right?
