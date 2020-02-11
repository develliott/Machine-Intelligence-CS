using System;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;
using CS_GA.Services;
using Ninject;
using Ninject.Syntax;

namespace CS_GA.Genetic_Algorithm
{
    public class SecondMain
    {
        public SecondMain(IEvolutionService evolutionService, IPopulationFactory populationFactory)
        {

            IPopulation population = populationFactory.GetPopulation(50);
            var geneticAlgorithm = new GeneticAlgorithm();

            // Evolve our population until we reach an near-optimal solution
            var generationCount = 0;
            var maxGenerations = 100000;

            var globalHighestScore = 0;
            var generationsWithNoChangeToScore = 0;
            var maxGenerationsWithNoChangeToScore = 500;

            // Simulate 'maxGenerations' amount of generations, unless the score hasn't changed for 'maxGenerationsWithNoChangeToScore'.
            // while (generationCount < maxGenerations &&
            //        generationsWithNoChangeToScore < maxGenerationsWithNoChangeToScore)
            // {
            //     generationCount++;
            //
            //     // Find the fittest individual in the current population.
            //     var fittestScoreInPop = population.GetFittestIndividual().GetFitness();
            //
            //     // If the score hasn't improved since the last generation ->
            //     if (fittestScoreInPop == globalHighestScore)
            //         // -> Increment 'generationsWithNoChangeToScore' by 1.
            //         generationsWithNoChangeToScore++;
            //     else
            //         // Reset 'generationsWithNoChangeToScore' to 0.
            //         generationsWithNoChangeToScore = 0;
            //
            //     // If the current fittest score is more than the global highest score ->
            //     if (fittestScoreInPop > globalHighestScore)
            //         // -> update the global highest score with the current fittest score.
            //         globalHighestScore = fittestScoreInPop;
            //
            //     Console.WriteLine("Generation: " + generationCount + " Fittest: " + fittestScoreInPop);
            //
            //     population = geneticAlgorithm.EvolvePopulation(population);
            // }


            Console.WriteLine("\nSolution found!");

            if (generationsWithNoChangeToScore == maxGenerationsWithNoChangeToScore)
                Console.WriteLine(
                    $"The score hasn't changed in {maxGenerationsWithNoChangeToScore} generations, so it has been considered an optimal solution.");

            Console.WriteLine("\nGeneration: " + generationCount);
            Console.WriteLine("Genes (Employee IDs):");
            // Console.WriteLine(population.GetFittestIndividual());
            // Console.WriteLine("Fitness: " + population.GetFittestIndividual().GetFitness());
        }
    }
}