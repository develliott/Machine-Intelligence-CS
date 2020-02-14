using System;
using CS_GA.Ninject;
using Ninject;

namespace CS_GA
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new Bindings());

            var runAlgorithmConstructor = kernel.Get<Algorithm>();

            Console.ReadLine();
        }
    }
}