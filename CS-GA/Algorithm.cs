using System;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;
using CS_GA.Services;

namespace CS_GA
{
    public class Algorithm
    {
        public Algorithm(IEvolutionService evolutionService, IEnvironmentService environmentService)
        {
            var population = environmentService.GenerateInitialisedPopulation(75);

            // Evolve our population until we reach an near-optimal solution
            var generationCount = 0;
            var maxGenerations = 100000;

            var globalHighestScore = 0;
            var generationsWithNoChangeToScore = 0;
            var maxGenerationsWithNoChangeToScore = 400;

            // Simulate 'maxGenerations' amount of generations, unless the score hasn't changed for 'maxGenerationsWithNoChangeToScore'.
            while (generationCount < maxGenerations &&
                   generationsWithNoChangeToScore < maxGenerationsWithNoChangeToScore)
            {
                generationCount++;

                var scoreOfFittestIndividual = population.MostSuitableIndividualToProblem.SuitabilityScore;

                // If the score hasn't improved since the last generation ->
                if (scoreOfFittestIndividual == globalHighestScore)
                    // -> Increment 'generationsWithNoChangeToScore' by 1.

                    // TODO: Check how many parts were high value assignments,
                    //       to influence the overwriting of better assignments 
                    generationsWithNoChangeToScore++;
                else
                    // Reset 'generationsWithNoChangeToScore' to 0.
                    generationsWithNoChangeToScore = 0;

                // If the 'scoreOfFittestIndividual' is more than the 'globalHighestScore' ->
                if (scoreOfFittestIndividual > globalHighestScore)
                    // -> update the 'globalHighestScore' with the 'scoreOfFittestIndividual'.
                    globalHighestScore = scoreOfFittestIndividual;

                // [ Purpose: To log out data less frequently.
                //   How: Sub-sample the data by logging an update every 'subSampleRate' generations.
                var subSampleRate = 10;
                if (generationCount % subSampleRate == 0)
                    Console.WriteLine($"Generation [{generationCount}] ~ Highest Fitness [{scoreOfFittestIndividual}]");

                population = evolutionService.EvolvePopulation(population);
                environmentService.UpdatePopulationSuitability(population);
                // ]
            }

            Console.WriteLine("\nSolution found!");

            if (generationsWithNoChangeToScore == maxGenerationsWithNoChangeToScore)
                Console.WriteLine(
                    $"The score hasn't changed in {maxGenerationsWithNoChangeToScore} generations, so it has been considered an optimal solution.");

            Console.WriteLine("\n\nGeneration: " + generationCount);
            Console.WriteLine("\n\nSolution\n--------");
            Console.WriteLine(population.MostSuitableIndividualToProblem);
            Console.WriteLine("\nFitness: " + population.MostSuitableIndividualToProblem.SuitabilityScore);
        }
    }
}