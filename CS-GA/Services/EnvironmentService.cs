using System;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;
using CS_GA.Business.GA_Data_Structure;

namespace CS_GA.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IStudentDataService<int> _studentDataService;

        public EnvironmentService(IStudentDataService<int> studentDataService)
        {
            _studentDataService = studentDataService;
        }

        public int UpdateIndividualSuitability(ref IIndividual individual)
        {
            var fitness = 0;

            for (var i = 0; i < individual.GeneLength; i++)
            {
                var studentIndex = individual.GetGeneValue(i);
                if (studentIndex != -1)
                {
                    var timeslotIndex = i;

                    fitness += _studentDataService.GetStudentPreference(studentIndex, timeslotIndex);
                }
            }

            individual.SuitabilityToProblem = fitness;

            return fitness;
        }

        public void UpdatePopulationSuitability(ref IPopulation population)
        {
            // Store a valid IIndividual so it's interface can be called without NullReference concerns.
            var mostSuitableIndividual = population.GetIndividual(0);

            for (var individualIndex = 0; individualIndex < population.Size; individualIndex++)
            {
                var currentIndividual = population.GetIndividual(individualIndex);
                UpdateIndividualSuitability(ref currentIndividual);

                if (currentIndividual.SuitabilityToProblem > mostSuitableIndividual.SuitabilityToProblem)
                    mostSuitableIndividual = currentIndividual;
            }

            population.MostSuitableIndividualToProblem = mostSuitableIndividual;
        }
    }

    public interface IEnvironmentService
    {
        int UpdateIndividualSuitability(ref IIndividual individual);
        void UpdatePopulationSuitability(ref IPopulation population);
    }
}