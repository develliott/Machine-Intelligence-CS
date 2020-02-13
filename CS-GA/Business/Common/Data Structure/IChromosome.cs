using System.Collections.Generic;

namespace CS_GA.Business.Common.Data_Structure
{
    public interface IChromosome
    {
        int Size { get; }
        void SetGeneValue(int geneIndex, int value);
        int GetGeneValue(int geneIndex);
        List<int> GetAssignedAlleles();
        void SetRandomUnassignedAllele(int unassignedIdentifier, int valueToReplaceWith);
        int[] GetGenes();
    }
}