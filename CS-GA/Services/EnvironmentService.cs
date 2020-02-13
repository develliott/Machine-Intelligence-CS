using System;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Data_Structure;
using CS_GA.Business.Common.Factories;

namespace CS_GA.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IStudentDataService<int> _studentDataService;
        private readonly IPopulationFactory _populationFactory;
        private readonly IProblemDomain _problemDomain;

        public EnvironmentService(IStudentDataService<int> studentDataService, IPopulationFactory populationFactory, IProblemDomain problemDomain)
        {
            _studentDataService = studentDataService;
            _populationFactory = populationFactory;
            _problemDomain = problemDomain;
        }

        public bool IsSolutionValid(IIndividual individual)
        {
            return _problemDomain.ValidateSolution(individual);
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

                    var score = _studentDataService.GetStudentPreference(studentIndex, timeslotIndex);

                    if (score == 10)
                        multiplier += 2;
                    else if (score == 1) multiplier += 1;

                    fitness += score;
                }
            }

            individual.SuitabilityScore = fitness + 1 * multiplier;
        }

        public IPopulation GenerateInitialisedPopulation(int size)
        {
            var population = _populationFactory.CreatePopulation(size);

            //TODO: Refactor - Population shouldn't have knowledge about how to initialise itself
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
    }

    public interface IEnvironmentService
    {
        void UpdatePopulationSuitability(IPopulation population);
        IPopulation GenerateInitialisedPopulation(int size);
        bool IsSolutionValid(IIndividual individual);
    }
}