using System.Collections.Generic;
using System.Linq;
using CS_GA.Business.Common;
using Ninject.Infrastructure.Language;

namespace CS_GA.Business.GA_Data_Structure
{
    public class Chromosome : IChromosome
    {
        private readonly int[] _genes;
        public int Size { get; }

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
            var numberOfStudents = 7;

            int[] requiredAlleles = Enumerable.Range(1, numberOfStudents).ToArray();

            bool validSolution = requiredAlleles.SequenceEqual(genesAsArray);

            return validSolution;
        }
    }
}