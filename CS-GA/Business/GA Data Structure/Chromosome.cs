using System.Collections.Generic;
using System.Linq;
using CS_GA.Business.Common;
using Ninject.Infrastructure.Language;

namespace CS_GA.Business.GA_Data_Structure
{
    public class Chromosome<T> : IChromosome<T>
    {
        private readonly T[] _genes;
        public int Size { get; }

        public Chromosome(int size)
        {
            Size = size;
            _genes = new T[Size];
        }


        public void SetGeneValue(int geneIndex, T value)
        {
            _genes[geneIndex] = value;
        }

        public T GetGeneValue(int geneIndex)
        {
            return _genes[geneIndex];
        }

        public bool GeneValueAlreadyAssigned(T geneValue)
        {
            return _genes.ToList().Exists(gene => gene.Equals(geneValue));
        }

        public void InitialiseChromosome(T outOfRangeValue)
        {
            for (var geneIndex = 0; geneIndex < Size; geneIndex++)
                SetGeneValue(geneIndex, outOfRangeValue);
        }
    }
}