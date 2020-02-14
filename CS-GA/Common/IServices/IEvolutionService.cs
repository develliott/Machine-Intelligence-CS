using CS_GA.Common.IData_Structure;

namespace CS_GA.Common.IServices
{
    public interface IEvolutionService
    {
        void SetValidIndividual(IIndividual individual);
        IPopulation EvolvePopulation(IPopulation oldPopulation);
    }
}