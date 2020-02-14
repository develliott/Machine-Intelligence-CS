using System;
using System.Collections.Generic;
using System.Linq;
using CS_GA.Common.IData_Structure;

namespace CS_GA.Business.Data_Structure
{
    public class Chromosome : IChromosome
    {
        private readonly int[] _genes;
        private readonly Random _random = new Random();

        public Chromosome(int size)
        {
            Size = size;
            _genes = new int[size];
        }

        public int Size { get; }


        public void SetGeneValue(int geneIndex, int value)
        {
            _genes[geneIndex] = value;
        }

        public int GetGeneValue(int geneIndex)
        {
            return _genes[geneIndex];
        }

        public int[] GetGenes()
        {
            return _genes;
        }

        public List<int> GetAssignedAlleles()
        {
            // Find all current assigned genes
            var genes = _genes.ToList();
            genes.RemoveAll(allele => allele.Equals(0));

            return genes;
        }

        public void SetRandomUnassignedAllele(int unassignedIdentifier, int alleleToReplaceWith)
        {
            // Find the index of every unassigned allele in the genes.
            var indicesOfUnassignedAlleles = Enumerable.Range(0, Size)
                .Where(allele => _genes[allele] == unassignedIdentifier)
                .ToList();

            var randomIndex = _random.Next(indicesOfUnassignedAlleles.Count);
            SetGeneValue(indicesOfUnassignedAlleles[randomIndex], alleleToReplaceWith);
        }

        public bool GeneValueAlreadyAssigned(int geneValue)
        {
            return _genes.ToList().Exists(gene => gene.Equals(geneValue));
        }
    }
}