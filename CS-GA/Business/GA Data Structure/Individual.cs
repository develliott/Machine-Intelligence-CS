using System.Collections.Generic;
using System.Linq;
using CS_GA.Business.Common;
using CS_GA.Services;

namespace CS_GA.Business.GA_Data_Structure
{
    public class Individual : IIndividual
    {
        private readonly IStudentDataService<int> _studentDataService;

        public IChromosome<int> Chromosome { get; }

        public int GeneLength => Chromosome.Size;

        private List<int> _tabuGenes;

        public int SuitabilityToProblem { get; set; }


        public Individual(IStudentDataService<int> studentDataService)
        {
            _studentDataService = studentDataService;
            
            Chromosome = new Chromosome<int>(studentDataService.MaxNumberOfTimeslots);
            _tabuGenes = new List<int>();
        }

        public int GetGeneValue(int geneIndex) => Chromosome.GetGeneValue(geneIndex);
        public void SetGeneValue(int geneIndex, int value)
        {
            if (!_tabuGenes.Contains(value))
            {
                Chromosome.SetGeneValue(geneIndex, value);
                _tabuGenes.Add(value);
            }
        }

        public void ClearAllele(int allele)
        {
            for (int geneIndex = 0; geneIndex < GeneLength; geneIndex++)
            {
                if (Chromosome.GetGeneValue(geneIndex) == allele)
                {
                    Chromosome.SetGeneValue(geneIndex, -1);
                }
            }
        }

        public List<int> GetValidAlleles()
        {
            IEnumerable<int> allPossibleGenes = Enumerable.Range(0, _studentDataService.MaxNumberOfStudents);
            IEnumerable<int> validGenes = allPossibleGenes.Except(_tabuGenes);
            return validGenes.ToList();
        }

        public bool IsGeneAlreadyAssigned(int geneValue)
        {
            List<int> validGenes = GetValidAlleles();

            return !validGenes.Contains(geneValue);
        }

        public void ClearTabuGenes() => _tabuGenes.Clear();

        public void RemoveFromTabu(int allele)
        {
            _tabuGenes.Remove(allele);
        }

        public void SwapAlleles(int allele1Index, int allele2Value)
        {
            int allele2Index = -1;
            int allele1Value = Chromosome.GetGeneValue(allele1Index);

            for (int geneIndex = 0; geneIndex < GeneLength; geneIndex++)
            {
                if (Chromosome.GetGeneValue(geneIndex) == allele2Value)
                {
                    allele2Index = geneIndex;
                }
            }

            Chromosome.SetGeneValue(allele1Index, allele2Value);
            Chromosome.SetGeneValue(allele2Index, allele1Value);

        }
    }
}