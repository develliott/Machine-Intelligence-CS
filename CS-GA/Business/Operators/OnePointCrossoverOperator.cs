using System;
using System.Collections.Generic;
using System.Text;
using CS_GA.Business.Common;

namespace CS_GA.Business.Operators
{
    public class OnePointCrossoverOperator : ICrossoverOperator
    {
        private readonly Random _random = new Random();

        public IIndividual PerformCrossover(IIndividual newIndividual, IIndividual individual1, IIndividual individual2)
        {
            var minimumCrossoverLength = 1;

            // Find a random point on the gene
            var crossoverIndex = _random.Next(0, newIndividual.GeneLength - minimumCrossoverLength);

            for (var geneIndex = 0; geneIndex < newIndividual.GeneLength; geneIndex++)
            {
                int allele;

                // Take genes from 'individual1' if the crossover point hasn't been reached
                if (geneIndex < crossoverIndex)
                    allele = individual1.GetGeneValue(geneIndex);
                // Take genes from 'individual2' when the crossover point is reached
                else
                    allele = individual2.GetGeneValue(geneIndex);

                newIndividual.SetGeneValue(geneIndex, allele);
            }

            // Ensure new individual is valid.
            newIndividual.CrossoverValidator();

            return newIndividual;
        }
    }
}