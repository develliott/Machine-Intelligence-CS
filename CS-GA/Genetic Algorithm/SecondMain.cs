using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Ninject;

namespace CS_GA.Genetic_Algorithm
{
    public class SecondMain
    {
        public SecondMain()
        {
            Population population = new Population(50, true);
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();

            // Evolve our population until we reach an near-optimal solution
            int generationCount = 0;
            int maxGenerations = 100000;

            int globalHighestScore = 0;
            int generationsWithNoChangeToScore = 0;
            int maxGenerationsWithNoChangeToScore = 500;

            // Simulate 'maxGenerations' amount of generations, unless the score hasn't changed for 'maxGenerationsWithNoChangeToScore'.
            while (generationCount < maxGenerations &&
                   generationsWithNoChangeToScore < maxGenerationsWithNoChangeToScore)
            {
                generationCount++;

                // Find the fittest individual in the current population.
                int fittestScoreInPop = population.GetFittestIndividual().GetFitness();

                // If the score hasn't improved since the last generation ->
                if (fittestScoreInPop == globalHighestScore)
                {
                    // -> Increment 'generationsWithNoChangeToScore' by 1.
                    generationsWithNoChangeToScore++;
                }
                else
                {
                    // Reset 'generationsWithNoChangeToScore' to 0.
                    generationsWithNoChangeToScore = 0;
                }

                // If the current fittest score is more than the global highest score ->
                if (fittestScoreInPop > globalHighestScore)
                {
                    // -> update the global highest score with the current fittest score.
                    globalHighestScore = fittestScoreInPop;
                }

                Console.WriteLine("Generation: " + generationCount + " Fittest: " + fittestScoreInPop);

                population = geneticAlgorithm.EvolvePopulation(population);
            }


            Console.WriteLine("\nSolution found!");

            if (generationsWithNoChangeToScore == maxGenerationsWithNoChangeToScore)
            {
                Console.WriteLine($"The score hasn't changed in {maxGenerationsWithNoChangeToScore} generations, so it has been considered an optimal solution.");
            }

            Console.WriteLine("\nGeneration: " + generationCount);
            Console.WriteLine("Genes (Employee IDs):");
            Console.WriteLine(population.GetFittestIndividual());
            Console.WriteLine("Fitness: " + population.GetFittestIndividual().GetFitness());

        }
    }
}
