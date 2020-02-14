using CS_GA.Common.IData_Structure;

namespace CS_GA.Common.IServices
{
    public interface IEvolutionService
    {
        IPopulation EvolvePopulation(IPopulation oldPopulation);
    }
}