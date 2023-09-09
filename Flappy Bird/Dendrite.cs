using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird
{
    internal class Dendrite
    {
        public Neuron Previous { get; }
        public Neuron Next { get; }
        public double Weight { get; set; }
        public double WeightUpdate { get; set; }


        public Dendrite(Neuron previous, Neuron next, double weight)
        {
            Previous = previous;
            Next = next;
            Weight = weight;
        }


        public void ApplyUpdates()
        {
            Weight += WeightUpdate;
            WeightUpdate = 0;
        }


        public double Compute()
        {

            return Previous.Output * Weight;
        }
    }
}

