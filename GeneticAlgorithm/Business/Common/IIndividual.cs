namespace CS_GA.Business.Common
{
    public interface IIndividual
    {
        int GeneLength { get; }
        int SuitabilityToProblem { get; set; }
        int GetGeneValue(int geneIndex);
        void SetGeneValue(int geneIndex, int value);
    }
}