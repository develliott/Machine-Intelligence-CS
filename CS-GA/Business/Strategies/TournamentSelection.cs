using System;
using CS_GA.Common.IData_Structure;
using CS_GA.Common.IFactories;
using CS_GA.Common.IStrategies;
using CS_GA.Services;

namespace CS_GA.Business.Strategies
{
    public class TournamentSelection : ISelectionStrategy
    {
        // TODO: Make this dynamic for the consumer
        private int tournamentSize = 10;

        private readonly IEnvironmentService _environmentService;
        private readonly IPopulationFactory _populationFactory;
        private readonly Random _random = new Random();

        public TournamentSelection(IEnvironmentService environmentService, IPopulationFactory populationFactory)
        {
            _environmentService = environmentService;
            _populationFactory = populationFactory;
        }

        /// <summary>
        /// Finds the individual with the highest suitability from a random population sample.
        /// </summary>
        /// <param name="population">The population to select from</param>
        /// <returns>The most suitable individual from the population sample</returns>
        public IIndividual SelectIndividualFromPopulation(IPopulation population)
        {
            var tournamentPopulation = _populationFactory.CreatePopulation(tournamentSize);

            for (var i = 0; i < tournamentPopulation.Size; i++)
            {
                var randomIndividualIndex = _random.Next(0, population.Size);
                var randomIndividual = population.GetIndividual(randomIndividualIndex);
                tournamentPopulation.SetIndividual(i, randomIndividual);
            }

            _environmentService.UpdatePopulationSuitability(tournamentPopulation);

            return tournamentPopulation.MostSuitableIndividualToProblem;
        }
    }
}