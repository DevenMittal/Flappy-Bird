using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Bird
{
    public class ActivationFunction
    {
        Func<double, double> function;
        Func<double, double> derivative;
        public ActivationFunction(Func<double, double> function, Func<double, double> derivative)
        {
            this.function = function;
            this.derivative = derivative;
        }

        public double Function(double input)
        {
            return function.Invoke(input);
        }

        public double Derivative(double input)
        {
            return derivative.Invoke(input);
        }
    }


    public class ErrorFunction
    {
        Func<double, double, double> function;
        Func<double, double, double> derivative;
        public ErrorFunction(Func<double, double, double> function, Func<double, double, double> derivative)
        {
            this.function = function;
            this.derivative = derivative;
        }

        public double Function(double output, double desiredOutput)
        {
            return function.Invoke(output, desiredOutput);
        }
        public double Derivative(double output, double desiredOutput)
        {
            return derivative.Invoke(output, desiredOutput);

        }
    }
}
