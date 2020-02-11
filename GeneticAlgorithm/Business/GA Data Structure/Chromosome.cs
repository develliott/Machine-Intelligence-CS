using System.Linq;
using CS_GA.Business.Common;

namespace CS_GA.Business.GA_Data_Structure
{
    public class Chromosome<T> : IChromosome<T>
    {
        private readonly Gene<T>[] _genes;

        public Chromosome(int size)
        {
            Size = size;
            _genes = new Gene<T>[Size];
        }

        public int Size { get; }

        public void SetGeneValue(int geneIndex, T value)
        {
            _genes[geneIndex].SetAllele(value);
        }

        public T GetGeneValue(int geneIndex)
        {
            return _genes[geneIndex].Allele;
        }

        public bool GeneValueAlreadyAssigned(T geneValue)
        {
            return _genes.ToList().Exists(gene => gene.Allele.Equals(geneValue));
        }

        public void InitialiseChromosome(T outOfRangeValue)
        {
            for (var geneIndex = 0; geneIndex < Size; geneIndex++)
                SetGeneValue(geneIndex, outOfRangeValue);
        }
    }
}