using System;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;
using CS_GA.Ninject;
using Ninject;
using Ninject.Parameters;

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