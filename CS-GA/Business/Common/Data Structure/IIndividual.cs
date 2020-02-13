using System.Collections.Generic;

namespace CS_GA.Business.Common.Data_Structure
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