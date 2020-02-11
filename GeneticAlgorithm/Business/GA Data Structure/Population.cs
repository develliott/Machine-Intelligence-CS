using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;

namespace CS_GA.Business.GA_Data_Structure
{
    public class Population : IPopulation
    {
        public IIndividual MostSuitableIndividualToProblem { get; set; }
        private IIndividual[] Individuals { get; }
        public int Size { get; }

        private readonly IIndividualFactory _individualFactory;

        public Population(int size, IIndividualFactory individualFactory)
        {
            Size = size;
            _individualFactory = individualFactory;

            Individuals = new IIndividual[Size];
        }
        
        public void InitialisePopulation()
        {
            for (var individualIndex = 0; individualIndex < Size; individualIndex++)
                Individuals[individualIndex] = _individualFactory.GetIndividual();
        }

        public IIndividual GetIndividual(int populationIndex)
        {
            return Individuals[populationIndex];
        }

        public void SetIndividual(int populationIndex, IIndividual individual)
        {
            Individuals[populationIndex] = individual;
        }
    }
}