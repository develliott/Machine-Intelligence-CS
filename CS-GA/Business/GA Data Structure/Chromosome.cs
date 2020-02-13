using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using CS_GA.Business.Common;
using Ninject.Infrastructure.Language;

namespace CS_GA.Business.GA_Data_Structure
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
                List<int> genes = _genes.ToList();
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

            List<int> genes = _genes.ToList();
            genes.RemoveAll(allele => allele.Equals(0));
            genes.Sort();
            int[] genesAsArray = genes.ToArray();

            //TODO: Change this when using final data!
            //TODO: Dynamically load this data
            var numberOfStudents = 13;

            int[] requiredAlleles = Enumerable.Range(1, numberOfStudents).ToArray();

            bool validSolution = requiredAlleles.SequenceEqual(genesAsArray);

            return validSolution;
        }

        public void ReplaceRandomZeroWithAllele( int alleleToReplaceWith)
        {

            var indicesOfZeros = Enumerable.Range(0, Size)
                .Where(i => _genes[i] == 0)
                .ToList();


            var randomIndex = _random.Next(indicesOfZeros.Count);

            SetGeneValue(indicesOfZeros[randomIndex], alleleToReplaceWith);
        }
    }
}