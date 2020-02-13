using System;
using System.Collections.Generic;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;
using CS_GA.Business.Operators;

namespace CS_GA.Services
{
    public class EvolutionService : IEvolutionService
    {
        private readonly bool _elitism = true;
        private readonly IEnvironmentService _environmentService;
        private readonly IIndividualFactory _individualFactory;
        private readonly IPopulationFactory _populationFactory;

        private readonly Random _random = new Random();
        private readonly IStudentDataService<int> _studentDataService;
        private readonly int tournamentSize = 10;

        private readonly IMutationOperator _mutationOperator = new SwapMutationOperator();
        private readonly ICrossoverOperator _crossoverOperator = new OnePointCrossoverOperator();

        public EvolutionService(IPopulationFactory populationFactory, IIndividualFactory individualFactory,
            IEnvironmentService environmentService, IStudentDataService<int> studentDataService)
        {
            _populationFactory = populationFactory;
            _individualFactory = individualFactory;
            _environmentService = environmentService;
            _studentDataService = studentDataService;
        }

        public void SetValidIndividual(IIndividual individual)
        {
            var tabuIndices = new List<int>();

            while (tabuIndices.Count < _studentDataService.MaxNumberOfStudents)
            {
                // Find a random index that hasn't been set yet.
                var randomIndex = _random.Next(_studentDataService.MaxNumberOfTimeSlots);
                while (tabuIndices.Contains(randomIndex))
                {
                    randomIndex = _random.Next(_studentDataService.MaxNumberOfTimeSlots);
                }
                tabuIndices.Add(randomIndex);


                var validAlleles = individual.GetValidAlleles();

                if (validAlleles.Count > 0)
                {
                    int newAllele;
                    if (validAlleles.Count > 1)
                    {
                        var randomValidAlleleIndex = _random.Next(0, validAlleles.Count);
                        newAllele = validAlleles[randomValidAlleleIndex];
                    }
                    else
                    {
                        newAllele = validAlleles[0];
                    }

                    individual.SetGeneValue(randomIndex, newAllele);
                }
            }
        }

        public IPopulation EvolvePopulation(IPopulation population)
        {
            var newPopulation = _populationFactory.CreatePopulation(population.Size);

            var individualIndexOffset = 0;
            if (_elitism)
            {
                newPopulation.SetIndividual(0, population.MostSuitableIndividualToProblem);
                individualIndexOffset = 1;
            }

            for (var individualIndex = individualIndexOffset; individualIndex < population.Size; individualIndex++)
            {
                // TODO: Refactor into _crossoverOperator.PerformCrossover
                var freshIndividual = _individualFactory.CreateIndividual();

                var individual1 = tournamentSelection(population);

                var individual2 = tournamentSelection(population);

                var resultingIndividualFromCrossoverOperation = _crossoverOperator.PerformCrossover(freshIndividual, individual1, individual2);

                newPopulation.SetIndividual(individualIndex, resultingIndividualFromCrossoverOperation);
            }

            // Mutate
            for (var individualIndex = individualIndexOffset; individualIndex < population.Size; individualIndex++)
            {
                // TODO: Refactor into _mutationOperator.PerformMutation
                int maxNumberOfMutations = 7;
                int randomMutationLimit = _random.Next(maxNumberOfMutations);

                for (int numberOfMutations = 0; numberOfMutations < randomMutationLimit; numberOfMutations++)
                {
                    _mutationOperator.PerformMutation(newPopulation.GetIndividual(individualIndex).Chromosome);
                }
            }

            return newPopulation;
        }

        private IIndividual tournamentSelection(IPopulation population)
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

    public interface IEvolutionService
    {
        void SetValidIndividual(IIndividual individual);
        IPopulation EvolvePopulation(IPopulation population);
    }
}