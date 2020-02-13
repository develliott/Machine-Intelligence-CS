using System;
using CS_GA.Business.Common;

namespace CS_GA.Business.Operators
{
    public class SwapMutationOperator : IMutationOperator
    {
        private readonly Random _random = new Random();

        // TODO: Refactor into _mutationOperator.PerformMutation
        private int _maxNumberOfMutations = 7;

        public IChromosome PerformMutation(IChromosome chromosome)
        {
            var randomMutationLimit = _random.Next(_maxNumberOfMutations);

            for (var numberOfMutations = 0; numberOfMutations < randomMutationLimit; numberOfMutations++)
            {
                // TODO: Implement infinity searching with boundary constraints

                // Find two indices at random.
                var swapIndex1 = _random.Next(chromosome.Size);
                var swapIndex2 = _random.Next(chromosome.Size);

                // Ensure indices are unique.
                while (swapIndex1 == swapIndex2) swapIndex2 = _random.Next(chromosome.Size);

                // Store the original values.
                var swapIndex1Value = chromosome.GetGeneValue(swapIndex1);
                var swapIndex2Value = chromosome.GetGeneValue(swapIndex2);

                // Swap the index's values.
                chromosome.SetGeneValue(swapIndex1, swapIndex2Value);
                chromosome.SetGeneValue(swapIndex2, swapIndex1Value);
            }

            return chromosome;
        }
    }
}