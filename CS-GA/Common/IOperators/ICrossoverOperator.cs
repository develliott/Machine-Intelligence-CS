using CS_GA.Common.IData_Structure;

namespace CS_GA.Common.IOperators
{
    public interface ICrossoverOperator
    {
        IIndividual PerformCrossover(IIndividual parent1, IIndividual parent2);
        IIndividual PerformCrossover(IPopulation population);
    }
}