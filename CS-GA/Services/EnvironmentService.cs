﻿using CS_GA.Common.IData_Structure;
using CS_GA.Common.IFactories;
using CS_GA.Common.IServices;

namespace CS_GA.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IPopulationFactory _populationFactory;
        private readonly IStudentDataService<int> _studentDataService;

        public EnvironmentService(IStudentDataService<int> studentDataService, IPopulationFactory populationFactory)
        {
            _studentDataService = studentDataService;
            _populationFactory = populationFactory;
        }

        public IPopulation GenerateInitialisedPopulation(int size)
        {
            var population = _populationFactory.CreatePopulation(size);
            population.InitialisePopulation();

            UpdatePopulationSuitability(population);

            return population;
        }

        public void UpdatePopulationSuitability(IPopulation population)
        {
            // Store a valid IIndividual so it's interface can be called without NullReference concerns.
            var mostSuitableIndividual = population.GetIndividual(0);

            for (var individualIndex = 0; individualIndex < population.Size; individualIndex++)
            {
                var currentIndividual = population.GetIndividual(individualIndex);
                UpdateIndividualSuitability(currentIndividual);

                if (currentIndividual.SuitabilityScore > mostSuitableIndividual.SuitabilityScore)
                    mostSuitableIndividual = currentIndividual;
            }

            population.MostSuitableIndividualToProblem = mostSuitableIndividual;
        }

        public int GetIndividualScore(int studentIndex, int timeSlotIndex)
        {
            return _studentDataService.GetStudentPreference(studentIndex, timeSlotIndex);
        }

        public void UpdateIndividualSuitability(IIndividual individual)
        {
            var fitness = 0;
            var multiplier = 0;

            for (var i = 0; i < individual.GeneLength; i++)
            {
                var studentIndex = individual.GetGeneValue(i) - 1;
                if (studentIndex != -1)
                {
                    var timeslotIndex = i;

                    var score = GetIndividualScore(studentIndex, timeslotIndex);

                    if (score == 10)
                        multiplier += 2;
                    else if (score == 1) multiplier += 1;

                    fitness += score;
                }
            }

            individual.SuitabilityScore = fitness + 1 * multiplier;
        }
    }
}