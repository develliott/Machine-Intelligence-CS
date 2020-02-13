using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS_GA.Business.Common;
using CS_GA.Services;

namespace CS_GA.Business.GA_Data_Structure
{
    public class Individual : IIndividual
    {
        private readonly IStudentDataService<int> _studentDataService;

        public IChromosome Chromosome { get; }

        public int GeneLength => Chromosome.Size;

        private List<int> _tabuGenes;

        public int SuitabilityToProblem { get; set; }


        public Individual(IStudentDataService<int> studentDataService)
        {
            _studentDataService = studentDataService;
            
            Chromosome = new Chromosome(studentDataService.MaxNumberOfTimeSlots);
            _tabuGenes = new List<int>();
        }

        public bool IsAValidSolution()
        {
            return Chromosome.IsAValidSolution();
        }

        public void CrossoverValidator()
        {

            if (!IsAValidSolution())
            {
                var assignedAlleles = Chromosome.GetAssignedAlleles();
                IEnumerable<int> allPossibleGenes = Enumerable.Range(1, _studentDataService.MaxNumberOfStudents);
                var missingAlleles = allPossibleGenes.Except(assignedAlleles).ToList();

                while (missingAlleles.Any())
                {
                    Chromosome.ReplaceRandomZeroWithAllele(missingAlleles[0]);
                    missingAlleles.Remove(missingAlleles[0]);
                }
            }

            if (!IsAValidSolution())
            {
                int s = 5;
            }
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
            IEnumerable<int> allPossibleGenes = Enumerable.Range(1, _studentDataService.MaxNumberOfStudents);
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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Time Slot Index:  ");
            for (int i = 0; i < GeneLength; i++)
            {
                stringBuilder.Append($"{i}  ");
            }

            stringBuilder.AppendLine();
            stringBuilder.Append("Student ID:       ");
            for (int i = 0; i < GeneLength; i++)
            {
                stringBuilder.Append($"{GetGeneValue(i)}  ");
            }

            return stringBuilder.ToString();
        }
    }
}