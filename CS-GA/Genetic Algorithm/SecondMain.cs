using System;
using System.Collections.Generic;
using System.Text;

namespace CS_GA.Genetic_Algorithm
{
    public class SecondMain
    {
        public SecondMain()
        {
            Population population = new Population(50, true);

            // Evolve our population until we reach an near-optimal solution
            int generationCount = 0;
            int maxGenerations = 100000;

            int globalHighestScore = 0;
            int generationsWithNoChangeToScore = 0;
            int maxGenerationsWithNoChangeToScore = 500;

            // Simulate 'maxGenerations' amount of generations, unless the score hasn't changed for 'maxGenerationsWithNoChangeToScore'.
            while (generationCount < maxGenerations && generationsWithNoChangeToScore < maxGenerationsWithNoChangeToScore)
            {
                generationCount++;

                // Find the fittest individual in the current population.
                int fittestScoreInPop = population.getFittest().getFitness();
            }            

    }
}
