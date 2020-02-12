using System.Collections.Generic;

namespace CS_GA.Business.Common
{
    public interface IIndividual
    {
        IChromosome<int> Chromosome { get; }
        int GeneLength { get; }
        int SuitabilityToProblem { get; set; }
        int GetGeneValue(int geneIndex);
        void SetGeneValue(int geneIndex, int value);
        List<int> GetValidAlleles();
        void ClearTabuGenes();
        bool IsGeneAlreadyAssigned(int geneValue);

        void ClearAllele(int allele);
        void RemoveFromTabu(int allele);
        void SwapAlleles(int allele1Index, int allele2Value);
    }
}