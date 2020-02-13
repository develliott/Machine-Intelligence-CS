using System;
using System.Collections.Generic;
using System.Text;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;
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

        public IIndividual PerformSelection(IPopulation population)
        {
            //TODO: Refactor into own ISelectionStrategy
            var tournamentPopulation = _populationFactory.CreatePopulation(tournamentSize);

            for (var i = 0; i < tournamentPopulation.Size; i++)
            {
                var randomIndividualIndex = _random.Next(0, population.Size);
                tournamentPopulation.SetIndividual(i, population.GetIndividual(randomIndividualIndex));
            }

            _environmentService.UpdatePopulationSuitability(tournamentPopulation);

            return tournamentPopulation.MostSuitableIndividualToProblem;
        }
    }
}