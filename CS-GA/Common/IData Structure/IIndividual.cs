using System.Collections.Generic;

namespace CS_GA.Common.IData_Structure
{
    public interface IIndividual
    {
        IChromosome Chromosome { get; }
        int GeneLength { get; }
        int SuitabilityScore { get; set; }
        int GetGeneValue(int geneIndex);
        void SetGeneValue(int geneIndex, int value);
        List<int> GetValidAlleles();
    }
}