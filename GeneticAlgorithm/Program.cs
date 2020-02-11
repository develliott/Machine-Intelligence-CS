using CS_GA.Business.Common;
using CS_GA.Genetic_Algorithm;
using CS_GA.Ninject;
using Ninject;

namespace CS_GA
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new Bindings());

            var secondMain = kernel.Get<SecondMain>();
        }
    }
}