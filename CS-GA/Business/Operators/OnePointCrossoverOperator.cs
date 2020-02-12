using System;
using System.Collections.Generic;
using System.Text;
using CS_GA.Business.Common;

namespace CS_GA.Business.Operators
{
    public class OnePointCrossoverOperator : ICrossoverOperator
    {
        Random _random = new Random();

        public IIndividual PerformCrossover(IIndividual newIndividual, IIndividual individual1, IIndividual individual2)
        {
            int crossoverIndex = _random.Next(newIndividual.GeneLength);

            for (int geneIndex = 0; geneIndex < newIndividual.GeneLength; geneIndex++)
            {
                int allele;

                if (geneIndex < crossoverIndex)
                {
                    allele = individual1.GetGeneValue(geneIndex);
                }
                else
                {
                    allele = individual2.GetGeneValue(geneIndex);
                }

                newIndividual.SetGeneValue(geneIndex, allele);

            }

            // TODO: Ensure new individual is valid.

            return newIndividual;
        }
    }
}
