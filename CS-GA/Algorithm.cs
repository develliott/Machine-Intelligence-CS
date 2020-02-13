using System;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;
using CS_GA.Services;

namespace CS_GA
{
    public class Algorithm
    {
        private readonly IEnvironmentService _environmentService;

        public Algorithm(IEvolutionService evolutionService, IPopulationFactory populationFactory, IEnvironmentService environmentService)
        {
            _environmentService = environmentService;

            IPopulation population = populationFactory.CreatePopulation(75);
            population.InitialisePopulation();
            _environmentService.UpdatePopulationSuitability(population);


            // Evolve our population until we reach an near-optimal solution
            var generationCount = 0;
            var maxGenerations = 100000;

            var globalHighestScore = 0;
            var generationsWithNoChangeToScore = 0;
            var maxGenerationsWithNoChangeToScore = 1000;

            // Simulate 'maxGenerations' amount of generations, unless the score hasn't changed for 'maxGenerationsWithNoChangeToScore'.
            while (generationCount < maxGenerations &&
                   generationsWithNoChangeToScore < maxGenerationsWithNoChangeToScore)
            {
                generationCount++;
                
                // Find the fittest individual in the current population.
                var fittestScoreInPop = population.MostSuitableIndividualToProblem.SuitabilityToProblem;
            
                // If the score hasn't improved since the last generation ->
                if (fittestScoreInPop == globalHighestScore)
                    // -> Increment 'generationsWithNoChangeToScore' by 1.
                    generationsWithNoChangeToScore++;
                else
                    // Reset 'generationsWithNoChangeToScore' to 0.
                    generationsWithNoChangeToScore = 0;
            
                // If the current fittest score is more than the global highest score ->
                if (fittestScoreInPop > globalHighestScore)
                    // -> update the global highest score with the current fittest score.
                    globalHighestScore = fittestScoreInPop;
            
                Console.WriteLine("Generation: " + generationCount + " Fittest: " + fittestScoreInPop);
            
                population = evolutionService.EvolvePopulation(population);
                _environmentService.UpdatePopulationSuitability(population);

            }

            Console.WriteLine("\nSolution found!");

            if (generationsWithNoChangeToScore == maxGenerationsWithNoChangeToScore)
                Console.WriteLine(
                    $"The score hasn't changed in {maxGenerationsWithNoChangeToScore} generations, so it has been considered an optimal solution.");

            Console.WriteLine("\nGeneration: " + generationCount);
            Console.WriteLine("Solution:");
            Console.WriteLine(population.MostSuitableIndividualToProblem);
            Console.WriteLine("Fitness: " + population.MostSuitableIndividualToProblem.SuitabilityToProblem);
        }
    }
}