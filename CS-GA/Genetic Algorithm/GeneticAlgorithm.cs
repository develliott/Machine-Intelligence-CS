using System;
using System.Collections.Generic;
using System.Text;

namespace CS_GA.Genetic_Algorithm
{
    public class GeneticAlgorithm
    {
        private double uniformRate = 0.5;
        private double mutationRate = 0.15;
        private int tournamentSize = 5;
        private bool elitism = true;

        public Population EvolvePopulation(Population pop)
        {
            Population newPopulation = new Population(pop.PopulationSize, false);

            int elitismOffset = 0;
            if (elitism)
            {
                newPopulation.SaveIndividual(0, pop.GetFittestIndividual());
                elitismOffset = 1;
            }

            // Crossover.
            for (int i = elitismOffset; i < pop.PopulationSize; i++)
            {
                Individual individual1 = tournamentSelection(pop);

                Individual individual2 = tournamentSelection(pop);

                Individual newIndividual = crossover(individual1, individual2);

                newPopulation.SaveIndividual(i, newIndividual);
            }

            // Mutate
            for (int i = elitismOffset; i < pop.PopulationSize; i++)
            {
                mutate(newPopulation.GetIndividual(i));
            }

            return newPopulation;
        }

        private void mutate(Individual individual)
        {
            for (int i = 0; i < individual.ChromosomeSize; i++)
            {
                Random r = new Random();

                if (r.NextDouble() <=  mutationRate)
                {
                    int newGene = individual.GetAllele(i);

                    while (individual.GeneAlreadyAssigned(newGene))
                    {
                        newGene = individual.GenerateNewGene();
                    }
                    individual.SetAllele(i, newGene);
                }
            }
        }

        private Individual crossover(Individual individual1, Individual individual2)
        {
            Individual newIndividual = new Individual();

            for (int geneIndex = 0; geneIndex < newIndividual.ChromosomeSize; geneIndex++)
            {
                int newGene = -1;
                Random r = new  Random();

                while (newIndividual.GeneAlreadyAssigned(newGene))
                {
                    if (r.NextDouble() <= uniformRate)
                    {
                        newGene = individual1.GetAllele(geneIndex);
                    }
                    else
                    {
                        newGene = individual2.GetAllele(geneIndex);
                    }

                    if (newIndividual.GeneAlreadyAssigned(individual1.GetAllele(geneIndex)) && newIndividual.GeneAlreadyAssigned(individual2.GetAllele(geneIndex)))
                    {
                        newGene = newIndividual.GenerateNewGene();
                    }

                }
                newIndividual.SetAllele(geneIndex, newGene);
            }

            return newIndividual;
        }

        private Individual tournamentSelection(Population pop)
        {
            Population tournament = new Population(tournamentSize, false);

            for (int i = 0; i < tournamentSize; i++)
            {
                Random r = new Random();
                int randomIndex = r.Next(0, pop.PopulationSize);
                tournament.SaveIndividual(i, pop.GetIndividual(randomIndex));
            }

            return tournament.GetFittestIndividual();
        }
    }
}
