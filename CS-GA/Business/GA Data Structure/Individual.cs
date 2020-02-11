using System.Collections.Generic;
using System.Linq;
using CS_GA.Business.Common;
using CS_GA.Services;

namespace CS_GA.Business.GA_Data_Structure
{
    public class Individual : IIndividual
    {
        private readonly IStudentDataService<int> _studentDataService;
        public int GeneLength => _chromosome.Size;

        private List<int> _tabuGenes;

        public int SuitabilityToProblem { get; set; }

        private readonly Chromosome<int> _chromosome;

        public Individual(IStudentDataService<int> studentDataService)
        {
            _studentDataService = studentDataService;
            
            _chromosome = new Chromosome<int>(studentDataService.MaxNumberOfTimeslots);
            _tabuGenes = new List<int>();
        }

        public int GetGeneValue(int geneIndex) => _chromosome.GetGeneValue(geneIndex);
        public void SetGeneValue(int geneIndex, int value)
        {
            if (!_tabuGenes.Contains(value))
            {
                _chromosome.SetGeneValue(geneIndex, value);
                _tabuGenes.Add(value);
            }
        }

        public void ClearAllele(int allele)
        {
            for (int geneIndex = 0; geneIndex < GeneLength; geneIndex++)
            {
                if (_chromosome.GetGeneValue(geneIndex) == allele)
                {
                    _chromosome.SetGeneValue(geneIndex, -1);
                }
            }
        }

        public List<int> GetValidGenes()
        {
            IEnumerable<int> allPossibleGenes = Enumerable.Range(0, _studentDataService.MaxNumberOfStudents);
            IEnumerable<int> validGenes = allPossibleGenes.Except(_tabuGenes);
            return validGenes.ToList();
        }

        public bool IsGeneAlreadyAssigned(int geneValue)
        {
            List<int> validGenes = GetValidGenes();

            return !validGenes.Contains(geneValue);
        }

        public void ClearTabuGenes() => _tabuGenes.Clear();
    }
}