using System;
using CS_GA.Business.Common;
using CS_GA.Business.GA_Data_Structure;

namespace CS_GA.Services
{
    public class EvolutionService : IEvolutionService
    {
        private Random _random = new Random();
        private readonly int _outOfRangeValue = -1;

        public void SetValidIndividual(IIndividual individual)
        {
            for (var geneIndex = 0; geneIndex < individual.GeneLength; geneIndex++)
            {
                var validGene = GenerateValidGene(individual);
                // chromosome.SetGeneValue(geneIndex, validGene);
            }
        }

        private object GenerateValidGene(IIndividual individual)
        {
            //TODO: This class shouldn't know that chromosome accepts ints - how to solve?
            // int newGeneValue = _random.Next(chromosome.ChromosomeConstraint.MaxGeneValue);
            var newGeneValue = 0;

            // if (individual .GeneValueAlreadyAssigned(newGeneValue)) newGeneValue = (int) GenerateValidGene(chromosome);

            return newGeneValue;
        }

        public void PopulateChromosome(ref Chromosome<object> chromosome)
        {
            throw new NotImplementedException();
        }
    }

    public interface IEvolutionService
    {
        void PopulateChromosome(ref Chromosome<object> chromosome);
    }
}