﻿using System;
using System.Collections.Generic;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Data_Structure;
using CS_GA.Business.Common.Factories;
using CS_GA.Business.Common.Operators;
using CS_GA.Business.Operators;

namespace CS_GA.Services
{
    public class EvolutionService : IEvolutionService
    {
        private readonly bool _elitism = true;
        private readonly IProblemDomain _problemDomain;
        private readonly IPopulationFactory _populationFactory;

        private readonly Random _random = new Random();
        private readonly IStudentDataService<int> _studentDataService;

        private readonly ICrossoverOperator _crossoverOperator;
        private readonly IMutationOperator _mutationOperator;


        public EvolutionService(IProblemDomain problemDomain, IPopulationFactory populationFactory, IStudentDataService<int> studentDataService, ICrossoverOperator crossoverOperator, IMutationOperator mutationOperator)
        {
            _problemDomain = problemDomain;
            _populationFactory = populationFactory;
            _studentDataService = studentDataService;
            _crossoverOperator = crossoverOperator;
            _mutationOperator = mutationOperator;
        }

        public void SetValidIndividual(IIndividual individual)
        {
            _problemDomain.MakeSolutionValid(individual);
        }

        public IPopulation EvolvePopulation(IPopulation oldPopulation)
        {
            var newPopulation = _populationFactory.CreatePopulation(oldPopulation.Size);

            var individualIndexOffset = 0;
            if (_elitism)
            {
                newPopulation.SetIndividual(0, oldPopulation.MostSuitableIndividualToProblem);
                individualIndexOffset = 1;
            }

            // Populate 'newPopulation' with children from the parents in 'oldPopulation' to exploit the current knowledge.
            for (var individualIndex = individualIndexOffset; individualIndex < newPopulation.Size; individualIndex++)
            {
                var resultingIndividualFromCrossoverOperation =
                    _crossoverOperator.PerformCrossover(oldPopulation);

                _problemDomain.MakeSolutionValid(resultingIndividualFromCrossoverOperation);

                newPopulation.SetIndividual(individualIndex, resultingIndividualFromCrossoverOperation);
            }

            // Mutate each individual in the new population to explore the problem domain.
            for (var individualIndex = individualIndexOffset; individualIndex < oldPopulation.Size; individualIndex++)
            {
                // TODO: Refactor the 'currentIndividual.Chromosome' - shouldn't be able to view Chromosome.
                var currentIndividual = newPopulation.GetIndividual(individualIndex);
                _mutationOperator.PerformMutation(currentIndividual.Chromosome);
            }

            return newPopulation;
        }
    }

    public interface IEvolutionService
    {
        void SetValidIndividual(IIndividual individual);
        IPopulation EvolvePopulation(IPopulation oldPopulation);
    }
}