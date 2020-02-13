using CS_GA.Business.Common.Data_Structure;

namespace CS_GA.Business.Common.Operators
{
    public interface ICrossoverOperator
    {
        IIndividual PerformCrossover(IIndividual parent1, IIndividual parent2);
        IIndividual PerformCrossover(IPopulation population);
    }
}