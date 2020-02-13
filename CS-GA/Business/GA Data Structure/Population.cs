using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;
using CS_GA.Services;

namespace CS_GA.Business.GA_Data_Structure
{
    public class Population : IPopulation
    {
        public IIndividual MostSuitableIndividualToProblem { get; set; }
        private IIndividual[] Individuals { get; }
        public int Size { get; }

        private readonly IIndividualFactory _individualFactory;
        private readonly IEvolutionService _evolutionService;

        public Population(int size, IIndividualFactory individualFactory, IEvolutionService evolutionService)
        {
            Size = size;
            _individualFactory = individualFactory;
            _evolutionService = evolutionService;

            Individuals = new IIndividual[Size];
        }

        public void InitialisePopulation()
        {
            for (var individualIndex = 0; individualIndex < Size; individualIndex++)
            {
                var individual = _individualFactory.CreateIndividual();
                _evolutionService.SetValidIndividual(individual);
                Individuals[individualIndex] = individual;
            }
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