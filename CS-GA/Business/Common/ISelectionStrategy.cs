using CS_GA.Business.Common.Data_Structure;

namespace CS_GA.Business.Common.Strategies
{
    public interface ISelectionStrategy
    {
        IIndividual SelectIndividualFromPopulation(IPopulation population);
    }
}
