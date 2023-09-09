using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird
{
    internal class NeuralNetwork
    {
        public Layer[] layers;
        ErrorFunction errorFunc;

        public NeuralNetwork(ActivationFunction activation, ErrorFunction errorFunc, params int[] neuronsPerLayer)
        {
            this.errorFunc = errorFunc;
            layers = new Layer[neuronsPerLayer.Length];
            Layer previous = null;

            for (int i = 0; i < neuronsPerLayer.Length; i++)
            {
                layers[i] = new Layer(activation, neuronsPerLayer[i], previous);
                previous = layers[i];
            }

        }
        public void Randomize(Random random, double min, double max)
        {
            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].Randomize(random, min, max);
            }
        }
        public double[] Compute(double[] inputs)
        {
            if (layers[0].Neurons.Length != inputs.Length)
            {
                throw new Exception("stop");
            }
            for (int i = 0; i < inputs.Length; i++)
            {
                layers[0].Neurons[i].Output = inputs[i];
            }
            double[] outputs = null;
            for (int i = 1; i < layers.Length; i++)
            {
                outputs = layers[i].Compute();
            }

            return outputs;
        }
        public double GetError(double[] inputs, double[] desiredOutputs)
        {
            double sum = 0;
            double[] results = Compute(inputs);
            for (int i = 0; i < results.Length; i++)
            {
                sum += errorFunc.Function(results[i], desiredOutputs[i]);
            }
            return sum;
        }

        void Backprop(double learningRate, double[] desiredOutputs)
        {

            for (int i = 0; i < layers[layers.Length - 1].Neurons.Length; i++)
            {
                layers[layers.Length - 1].Neurons[i].Delta += errorFunc.Derivative(layers[layers.Length - 1].Neurons[i].Output, desiredOutputs[i]);
            }
            for (int i = layers.Length - 1; i > 0; i--)
            {
                layers[i].Backprop(learningRate);
            }

        }
        public double TrainGradientDescent(double[][] inputs, double[][] desiredOutputs, double learingRate)
        {
            double totalError = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                totalError += GetError(inputs[i], desiredOutputs[i]);
                Backprop(learingRate, desiredOutputs[i]);
            }
            ApplyUpdates();
            return totalError;
        }
        public void ApplyUpdates()
        {
            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].ApplyUpdates();
            }
        }
    }
}
