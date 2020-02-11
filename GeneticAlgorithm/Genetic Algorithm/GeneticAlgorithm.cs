using System;

namespace CS_GA.Genetic_Algorithm
{
    public class GeneticAlgorithm
    {
        private readonly bool elitism = true;
        private readonly double mutationRate = 0.15;
        private readonly int tournamentSize = 5;
        private readonly double uniformRate = 0.5;

        public Population EvolvePopulation(Population pop)
        {
            var newPopulation = new Population(pop.PopulationSize, false);

            var elitismOffset = 0;
            if (elitism)
            {
                newPopulation.SaveIndividual(0, pop.GetFittestIndividual());
                elitismOffset = 1;
            }

            // Crossover.
            for (var i = elitismOffset; i < pop.PopulationSize; i++)
            {
                var individual1 = tournamentSelection(pop);

                var individual2 = tournamentSelection(pop);

                var newIndividual = crossover(individual1, individual2);

                newPopulation.SaveIndividual(i, newIndividual);
            }

            // Mutate
            for (var i = elitismOffset; i < pop.PopulationSize; i++) mutate(newPopulation.GetIndividual(i));

            return newPopulation;
        }

        private void mutate(Individual individual)
        {
            for (var i = 0; i < individual.ChromosomeSize; i++)
            {
                var r = new Random();

                if (r.NextDouble() <= mutationRate)
                {
                    var newGene = individual.GetAllele(i);

                    while (individual.GeneAlreadyAssigned(newGene)) newGene = individual.GenerateNewGene();
                    individual.SetAllele(i, newGene);
                }
            }
        }

        private Individual crossover(Individual individual1, Individual individual2)
        {
            var newIndividual = new Individual();

            for (var geneIndex = 0; geneIndex < newIndividual.ChromosomeSize; geneIndex++)
            {
                var newGene = -1;
                var r = new Random();

                while (newIndividual.GeneAlreadyAssigned(newGene))
                {
                    if (r.NextDouble() <= uniformRate)
                        newGene = individual1.GetAllele(geneIndex);
                    else
                        newGene = individual2.GetAllele(geneIndex);

                    if (newIndividual.GeneAlreadyAssigned(individual1.GetAllele(geneIndex)) &&
                        newIndividual.GeneAlreadyAssigned(individual2.GetAllele(geneIndex)))
                        newGene = newIndividual.GenerateNewGene();
                }

                newIndividual.SetAllele(geneIndex, newGene);
            }

            return newIndividual;
        }

        private Individual tournamentSelection(Population pop)
        {
            var tournament = new Population(tournamentSize, false);

            for (var i = 0; i < tournamentSize; i++)
            {
                var r = new Random();
                var randomIndex = r.Next(0, pop.PopulationSize);
                tournament.SaveIndividual(i, pop.GetIndividual(randomIndex));
            }

            return tournament.GetFittestIndividual();
        }
    }
}