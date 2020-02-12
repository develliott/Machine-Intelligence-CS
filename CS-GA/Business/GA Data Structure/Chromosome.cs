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

        public bool IsAValidSolution()
        {
            // Define a valid solution.
            //
            // 0 = Unassigned Time Slot
            // Each student must be assigned 1 time.

            List<T> genes = _genes.ToList();
            genes.RemoveAll(allele => allele.Equals(0));

            //TODO: Change this when using final data!
            //TODO: Dynamically load this data
            var numberOfStudents = 7;

            IList<int> requiredAlleles = Enumerable.Range(1, numberOfStudents).ToList();

            bool validSolution = requiredAlleles.Equals(genes);

            return validSolution;
        }
    }
}