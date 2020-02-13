using System;
using System.Collections.Generic;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;
using CS_GA.Business.Operators;
using CS_GA.Business.Strategies;

namespace CS_GA.Services
{
    public class EvolutionService : IEvolutionService
    {
        private readonly bool _elitism = true;
        private readonly IIndividualFactory _individualFactory;
        private readonly IPopulationFactory _populationFactory;

        private readonly Random _random = new Random();
        private readonly IStudentDataService<int> _studentDataService;

        private readonly ICrossoverOperator _crossoverOperator;
        private readonly IMutationOperator _mutationOperator;
        private readonly ISelectionStrategy _selectionStrategy;


        public EvolutionService(IPopulationFactory populationFactory, IIndividualFactory individualFactory, IStudentDataService<int> studentDataService, ICrossoverOperator crossoverOperator, IMutationOperator mutationOperator, ISelectionStrategy selectionStrategy)
        {
            _populationFactory = populationFactory;
            _individualFactory = individualFactory;
            _studentDataService = studentDataService;
            _crossoverOperator = crossoverOperator;
            _mutationOperator = mutationOperator;
            _selectionStrategy = selectionStrategy;
        }

        public void SetValidIndividual(IIndividual individual)
        {
            var tabuIndices = new List<int>();

            while (tabuIndices.Count < _studentDataService.MaxNumberOfStudents)
            {
                // Find a random index that hasn't been set yet.
                var randomIndex = _random.Next(_studentDataService.MaxNumberOfTimeSlots);
                while (tabuIndices.Contains(randomIndex))
                    randomIndex = _random.Next(_studentDataService.MaxNumberOfTimeSlots);
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

                var individual1 = _selectionStrategy.PerformSelection(population);

                var individual2 = _selectionStrategy.PerformSelection(population);

                var resultingIndividualFromCrossoverOperation =
                    _crossoverOperator.PerformCrossover(freshIndividual, individual1, individual2);

                newPopulation.SetIndividual(individualIndex, resultingIndividualFromCrossoverOperation);
            }

            // Mutate
            for (var individualIndex = individualIndexOffset; individualIndex < population.Size; individualIndex++)
            {
                // TODO: Refactor into _mutationOperator.PerformMutation
                var maxNumberOfMutations = 7;
                var randomMutationLimit = _random.Next(maxNumberOfMutations);

                for (var numberOfMutations = 0; numberOfMutations < randomMutationLimit; numberOfMutations++)
                    _mutationOperator.PerformMutation(newPopulation.GetIndividual(individualIndex).Chromosome);
            }

            return newPopulation;
        }

        // tournament

        // private IIndividual tournamentSelection(IPopulation population)
        // {
        //     //TODO: Refactor into own ISelectionStrategy
        //     var tournamentPopulation = _populationFactory.CreatePopulation(tournamentSize);
        //
        //     for (var i = 0; i < tournamentPopulation.Size; i++)
        //     {
        //         var randomIndividualIndex = _random.Next(0, population.Size);
        //         tournamentPopulation.SetIndividual(i, population.GetIndividual(randomIndividualIndex));
        //     }
        //
        //     _environmentService.UpdatePopulationSuitability(tournamentPopulation);
        //
        //     return tournamentPopulation.MostSuitableIndividualToProblem;
        // }
    }

    public interface IEvolutionService
    {
        void SetValidIndividual(IIndividual individual);
        IPopulation EvolvePopulation(IPopulation population);
    }
}