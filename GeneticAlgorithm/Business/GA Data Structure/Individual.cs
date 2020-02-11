using System.Collections.Generic;
using System.Linq;
using CS_GA.Business.Common;
using CS_GA.Services;

namespace CS_GA.Business.GA_Data_Structure
{
    public class Individual : IIndividual
    {
        public int GeneLength => _chromosome.Size;

        public List<int> _tabuList;

        public int SuitabilityToProblem { get; set; }

        private readonly int _outOfRangeValue = -1;

        private readonly Chromosome<int> _chromosome;

        public Individual(IStudentDataService<int> studentDataService)
        {
            _chromosome = new Chromosome<int>(studentDataService.MaxNumberOfTimeslots);
        }

        public int GetGeneValue(int geneIndex) => _chromosome.GetGeneValue(geneIndex);
        public void SetGeneValue(int geneIndex, int value)
        {
            if (!_tabuList.Contains(value))
            {
                _chromosome.SetGeneValue(geneIndex, value);
                _tabuList.Add(value);
            }
        }

        public List<int> GetValidGenes()
        {
            IEnumerable<int> allPossibleGenes = Enumerable.Range(0, GeneLength);

            return null;
        }
    }
}