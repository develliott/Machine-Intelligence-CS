using System;
using System.Collections.Generic;
using System.Text;
using CS_GA.Business.Common;

namespace CS_GA.Business.Operators
{
    public class SwapMutation : IMutationOperator
    {
        private Random _random = new Random();

        public IChromosome<int> Mutate(IChromosome<int> chromosome)
        {
            // TODO: Implement infinity searching with boundary constraints

            // === Find 2 indices at random.
            var swapIndex1 = _random.Next(chromosome.Size);
            var swapIndex2 = _random.Next(chromosome.Size);

            // Ensure indices are unique.
            while (swapIndex1 == swapIndex2)
            {
                swapIndex2 = _random.Next(chromosome.Size);
            }

            // Store the original values.
            var swapIndex1OriginalValue = chromosome.GetGeneValue(swapIndex1);
            var swapIndex2OriginalValue = chromosome.GetGeneValue(swapIndex2);

            // Swap the index values.
            chromosome.SetGeneValue(swapIndex1, swapIndex2OriginalValue);
            chromosome.SetGeneValue(swapIndex2, swapIndex1OriginalValue);

            return chromosome;
        }
    }
}
