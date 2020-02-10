using System;
using CS_GA.Data;
using CS_GA.Genetic_Algorithm;
using Ninject;
using Ninject.Parameters;

namespace CS_GA
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new Ninject.Bindings());

            var secondMain = kernel.Get<SecondMain>();
        }

       
    }
}