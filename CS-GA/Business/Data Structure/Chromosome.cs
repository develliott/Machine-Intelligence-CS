using System;
using System.Collections.Generic;
using System.Linq;
using CS_GA.Business.Common.Data_Structure;

namespace CS_GA.Business.Data_Structure
{
    public class Chromosome : IChromosome
    {
        private readonly int[] _genes;
        public int Size { get; }

        private Random _random = new Random();

        public Chromosome(int size)
        {
            Size = size;
            _genes = new int[Size];
        }


        public void SetGeneValue(int geneIndex, int value)
        {
            _genes[geneIndex] = value;
        }

        public int GetGeneValue(int geneIndex)
        {
            return _genes[geneIndex];
        }

        public List<int> GetAssignedAlleles()
        {
            // Find all current assigned genes
            var genes = _genes.ToList();
            genes.RemoveAll(allele => allele.Equals(0));

            return genes;
        }

        public bool GeneValueAlreadyAssigned(int geneValue)
        {
            return _genes.ToList().Exists(gene => gene.Equals(geneValue));
        }

        public void InitialiseChromosome(int outOfRangeValue)
        {
            for (var geneIndex = 0; geneIndex < Size; geneIndex++)
                SetGeneValue(geneIndex, outOfRangeValue);
        }

        public bool IsAValidSolution()
        {
            // Define a valid solution.
            //
            // 0 = Unassigned Time Slot
            // Each student must be assigned 1 time.

            var genes = _genes.ToList();
            genes.RemoveAll(allele => allele.Equals(0));
            genes.Sort();
            var genesAsArray = genes.ToArray();

            //TODO: Change this when using final data!
            //TODO: Dynamically load this data
            var numberOfStudents = 13;

            var requiredAlleles = Enumerable.Range(1, numberOfStudents).ToArray();

            var validSolution = requiredAlleles.SequenceEqual(genesAsArray);

            return validSolution;
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
    }
}