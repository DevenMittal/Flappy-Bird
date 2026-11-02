using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird
{
    internal class GeneticAlgorithm
    {
        public void Mutate(NeuralNetwork net, Random random, double mutationRate)
        {
            int count = 0;
            foreach (Layer layer in net.layers)
            {
                count++;
                if (count != 1)
                {
                    foreach (Neuron neuron in layer.Neurons)
                    {
                        //Mutate the Weights
                        for (int i = 0; i < neuron.dendrites.Length; i++)
                        {
                            if (random.NextDouble() < mutationRate)
                            {
                                if (random.Next(2) == 0)
                                {
                                    neuron.dendrites[i].Weight *= random.NextDouble() * (1.5 - .5) + .5; //scale weight
                                }
                                else
                                {
                                    neuron.dendrites[i].Weight *= -1; //flip sign
                                }
                            }
                        }

                        //Mutate the Bias
                        if (random.NextDouble() < mutationRate)
                        {
                            if (random.Next(2) == 0)
                            {
                                neuron.bias *= random.NextDouble() * (1.5 - .5) + .5; //scale weight
                            }
                            else
                            {
                                neuron.bias *= -1; //flip sign
                            }
                        }
                    }
                }
            }
        }
        public void Crossover(NeuralNetwork firstParent, NeuralNetwork secondParent, NeuralNetwork child, Random random)
        {
            for (int i = 1; i < firstParent.layers.Length; i++)
            {
                Layer firstLayer = firstParent.layers[i];
                Layer secondLayer = secondParent.layers[i];
                Layer childLayer = child.layers[i];

                for (int j = 0; j < firstLayer.Neurons.Length; j++)
                {
                    Neuron firstNeuron = firstLayer.Neurons[j];
                    Neuron secondNeuron = secondLayer.Neurons[j];
                    Neuron childNeuron = childLayer.Neurons[j];

                    childNeuron.bias = random.Next(2) == 0 ? firstNeuron.bias : secondNeuron.bias;
                    for (int d = 0; d < firstNeuron.dendrites.Length; d++)
                    {
                        childNeuron.dendrites[d].Weight = random.Next(2) == 0
                            ? firstNeuron.dendrites[d].Weight
                            : secondNeuron.dendrites[d].Weight;
                    }
                }
            }
        }

        public void TrainGeneticLearning(Bird[] birds, Random random, double mutationRate)
        {
            Array.Sort(birds, (a, b) => b.fitness.CompareTo(a.fitness));
            int eliteCount = 1;

            //Keep the farthest bird, then make every other bird from that elite parent.
            for (int i = eliteCount; i < birds.Length; i++)
            {
                int firstParentIndex = random.Next(eliteCount);
                Crossover(birds[firstParentIndex].brain, birds[firstParentIndex].brain, birds[i].brain, random);
                Mutate(birds[i].brain, random, mutationRate);
            }
        }

    }
}
