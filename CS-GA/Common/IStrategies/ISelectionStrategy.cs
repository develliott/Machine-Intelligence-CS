using CS_GA.Common.IData_Structure;

namespace CS_GA.Common.IStrategies
{
    public interface ISelectionStrategy
    {
        IIndividual SelectIndividualFromPopulation(IPopulation population);
    }
}