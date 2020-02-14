using CS_GA.Common.IData_Structure;

namespace CS_GA.Common.IServices
{
    public interface IEnvironmentService
    {
        void UpdatePopulationSuitability(IPopulation population);
        IPopulation GenerateInitialisedPopulation(int size);
        int GetIndividualScore(int studentIndex, int timeSlotIndex);
    }
}