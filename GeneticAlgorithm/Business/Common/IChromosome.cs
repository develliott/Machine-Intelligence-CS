namespace CS_GA.Business.Common
{
    public interface IChromosome<T>
    {
        int Size { get; }
        void SetGeneValue(int geneIndex, T value);
        T GetGeneValue(int geneIndex);
        bool GeneValueAlreadyAssigned(T geneValue);
        void InitialiseChromosome(T outOfRangeValue);
    }
}