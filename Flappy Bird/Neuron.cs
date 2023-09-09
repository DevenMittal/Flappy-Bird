using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird
{
    internal class Neuron
    {
        public double bias;
        public Dendrite[] dendrites;
        public double Output { get; set; }
        public double Input { get; private set; }
        public ActivationFunction Activation { get; set; }
        public double Delta { get; set; }

        double biasUpdate;



        public Neuron(ActivationFunction activation, Neuron[] previousNerons)
        {
            Activation = activation;
            if (previousNerons != null)
            {
                dendrites = new Dendrite[previousNerons.Length];
                for (int i = 0; i < dendrites.Length; i++)
                {
                    dendrites[i] = new Dendrite(previousNerons[i], this, 0);
                }
                Randomize(new Random(), -1.0, 1.0);
            }
            else
            {
                dendrites = null;
            }
        }


        public void ApplyUpdates()
        {
            for (int i = 0; i < dendrites.Length; i++)
            {
                dendrites[i].ApplyUpdates();
            }
        }


        public void Backprop(double learningRate)
        {

            for (int i = 0; i < dendrites.Length; i++)
            {
                dendrites[i].Previous.Delta += Delta * Activation.Derivative(Input) * dendrites[i].Weight;
                dendrites[i].WeightUpdate -= Delta * Activation.Derivative(Input) * learningRate * dendrites[i].Previous.Output;
            }
            bias -= learningRate * Activation.Derivative(Input) * Delta;
            Delta = 0;
        }




        public void Randomize(Random random, double min, double max)
        {
            for (int i = 0; i < dendrites.Length; i++)
            {
                dendrites[i].Weight = random.NextDouble() * (max - min) + min;
            }
        }
        public double Compute()
        {
            Input = bias;
            for (int i = 0; i < dendrites.Length; i++)
            {
                Input += dendrites[i].Compute();
            }

            Output = Activation.Function(Input);
            return Output;
        }
    }
}

