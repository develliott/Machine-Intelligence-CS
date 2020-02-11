using System.Collections.Generic;

namespace CS_GA.Business.Common
{
    public interface IIndividual
    {
        int GeneLength { get; }
        int SuitabilityToProblem { get; set; }
        int GetGeneValue(int geneIndex);
        void SetGeneValue(int geneIndex, int value);
        List<int> GetValidGenes();
        void ClearTabuGenes();
        bool IsGeneAlreadyAssigned(int geneValue);

        void ClearAllele(int allele);
    }
}