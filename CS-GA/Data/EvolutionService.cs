using System;
using System.Collections.Generic;
using System.Text;

namespace CS_GA.Data
{
    public class EvolutionService : IEvolutionService
    {
        private Random _random = new Random();
        public void PopulateChromosome(ref Chromosome<object> chromosome)
        {
            for (int geneIndex = 0; geneIndex < chromosome.Size; geneIndex++)
            {
                object validGene = GenerateValidGene(chromosome);
                chromosome.SetGeneValueAtIndex(geneIndex, validGene);
            }
        }

        private object GenerateValidGene(Chromosome<object> chromosome)
        {
            //TODO: This class shouldn't know that chromosome accepts ints - how to solve?
            int newGeneValue = _random.Next(chromosome.ChromosomeConstraint.MaxGeneValue);

            if (chromosome.GeneValueAlreadyAssigned(newGeneValue))
            {
                newGeneValue = (int) GenerateValidGene(chromosome);
            }

            return newGeneValue;
        }
    }

    public interface IEvolutionService
    {
        void PopulateChromosome(ref Chromosome<object> chromosome);
    }
}
