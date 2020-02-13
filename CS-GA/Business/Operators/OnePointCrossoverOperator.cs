using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Data_Structure;
using CS_GA.Business.Common.Factories;
using CS_GA.Business.Common.Operators;
using CS_GA.Business.Common.Strategies;
using CS_GA.Business.Strategies;

namespace CS_GA.Business.Operators
{
    public class OnePointCrossoverOperator : ICrossoverOperator
    {
        private readonly IIndividualFactory _individualFactory;
        private readonly ISelectionStrategy _selectionStrategy;
        private readonly Random _random = new Random();

        public OnePointCrossoverOperator( IIndividualFactory individualFactory, ISelectionStrategy selectionStrategy)
        {
            _individualFactory = individualFactory;
            _selectionStrategy = selectionStrategy;
        }

        /// <summary>
        /// Selects two individuals from the population and performs the crossover operation on them.
        /// </summary>
        /// <param name="population">The population to select from</param>
        /// <returns></returns>
        public IIndividual PerformCrossover(IPopulation population)
        {
            var individual1 = _selectionStrategy.SelectIndividualFromPopulation(population);
            var individual2 = _selectionStrategy.SelectIndividualFromPopulation(population);

            return PerformCrossover(individual1, individual2);
        }

        /// <summary>
        /// Perform a random splice of the genes from 'parent1' and 'parent2' to produce a child.
        /// </summary>
        /// <param name="parent1">The parent to take the first selection of genes from</param>
        /// <param name="parent2">The parent to take the second selection of genes from</param>
        /// <returns></returns>
        public IIndividual PerformCrossover(IIndividual parent1, IIndividual parent2)
        {
            var child = _individualFactory.CreateIndividual();

            // Select a random index for the crossover point.
            // TODO: Make this dynamic
            var minimumCrossoverLength = 1;
            var crossoverIndex = _random.Next(0, child.GeneLength - minimumCrossoverLength);

            for (var geneIndex = 0; geneIndex < child.GeneLength; geneIndex++)
            {
                int alleleFromParent;

                // Take genes from 'parent1' if the crossover point hasn't been reached
                if (geneIndex < crossoverIndex)
                    alleleFromParent = parent1.GetGeneValue(geneIndex);

                // Take genes from 'parent2' if the crossover point has been reached
                else
                    alleleFromParent = parent2.GetGeneValue(geneIndex);

                child.SetGeneValue(geneIndex, alleleFromParent);
            }

            return child;
        }


    }
}