using CS_GA.Common.IData_Structure;

namespace CS_GA.Common.IFactories
{
    public interface IPopulationFactory
    {
        IPopulation CreatePopulation(int size);
    }
}