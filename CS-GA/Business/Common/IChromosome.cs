using System.Collections.Generic;

namespace CS_GA.Business.Common
{
    public interface IChromosome
    {
        int Size { get; }
        void SetGeneValue(int geneIndex, int value);
        int GetGeneValue(int geneIndex);
        bool GeneValueAlreadyAssigned(int geneValue);
        void InitialiseChromosome(int outOfRangeValue);
        bool IsAValidSolution();
        List<int> GetAssignedAlleles();
        void ReplaceRandomZeroWithAllele(int valueToReplaceWith);
    }
}