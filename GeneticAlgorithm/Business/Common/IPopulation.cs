namespace CS_GA.Business.Common
{
    public interface IPopulation
    {
        int Size { get; }
        void InitialisePopulation();
        IIndividual GetIndividual(int populationIndex);
        void SetIndividual(int populationIndex, IIndividual individual);
    }
}