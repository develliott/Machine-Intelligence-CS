using System;
using CS_GA.Common.IData_Structure;
using CS_GA.Common.IOperators;

namespace CS_GA.Business.Operators
{
    public class SwapMutationOperator : IMutationOperator
    {
        private readonly int _maxNumberOfMutations = 7;
        private readonly Random _random = new Random();

        public IIndividual PerformMutation(IIndividual individual)
        {
            var randomMutationLimit = _random.Next(_maxNumberOfMutations);

            for (var numberOfMutations = 0; numberOfMutations < randomMutationLimit; numberOfMutations++)
            {
                // TODO: Implement infinity searching with boundary constraints

                // Find two indices at random.
                var swapIndex1 = _random.Next(individual.Chromosome.Size);
                var swapIndex2 = _random.Next(individual.Chromosome.Size);

                // Ensure indices are unique.
                while (swapIndex1 == swapIndex2) swapIndex2 = _random.Next(individual.Chromosome.Size);

                // Store the original values.
                var swapIndex1Value = individual.GetGeneValue(swapIndex1);
                var swapIndex2Value = individual.GetGeneValue(swapIndex2);

                // Swap the index's values.
                individual.SetGeneValue(swapIndex1, swapIndex2Value);
                individual.SetGeneValue(swapIndex2, swapIndex1Value);
            }

            return individual;
        }
    }
}