using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_GA.Data
{
    public class Chromosome<T>
    {
        public ChromosomeConstraint ChromosomeConstraint { get; }
        public int Size { get;}

        private Gene<T>[] _genes;

        public Chromosome(int size, ChromosomeConstraint chromosomeConstraint)
        {
            ChromosomeConstraint = chromosomeConstraint;
            Size = size;
            _genes = new Gene<T>[Size];
        }

        public void SetGeneValueAtIndex(int geneIndex, T value)
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
    }
}
