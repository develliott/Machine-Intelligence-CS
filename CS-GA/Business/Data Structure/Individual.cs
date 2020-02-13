using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS_GA.Business.Common.Data_Structure;
using CS_GA.Services;

namespace CS_GA.Business.Data_Structure
{
    public class Individual : IIndividual
    {
        private readonly IStudentDataService<int> _studentDataService;

        public IChromosome Chromosome { get; }

        public int GeneLength => Chromosome.Size;

        private List<int> _tabuGenes;

        public int SuitabilityScore { get; set; }


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
                var allPossibleGenes = Enumerable.Range(1, _studentDataService.MaxNumberOfStudents);
                var missingAlleles = allPossibleGenes.Except(assignedAlleles).ToList();

                while (missingAlleles.Any())
                {
                    Chromosome.SetRandomUnassignedAllele(0, missingAlleles[0]);
                    missingAlleles.Remove(missingAlleles[0]);
                }
            }

            if (!IsAValidSolution())
            {
                var s = 5;
            }
        }

        public int GetGeneValue(int geneIndex)
        {
            return Chromosome.GetGeneValue(geneIndex);
        }

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
            for (var geneIndex = 0; geneIndex < GeneLength; geneIndex++)
                if (Chromosome.GetGeneValue(geneIndex) == allele)
                    Chromosome.SetGeneValue(geneIndex, -1);
        }

        public List<int> GetValidAlleles()
        {
            var allPossibleGenes = Enumerable.Range(1, _studentDataService.MaxNumberOfStudents);
            var validGenes = allPossibleGenes.Except(_tabuGenes);
            return validGenes.ToList();
        }

        public bool IsGeneAlreadyAssigned(int geneValue)
        {
            var validGenes = GetValidAlleles();

            return !validGenes.Contains(geneValue);
        }

        public void ClearTabuGenes()
        {
            _tabuGenes.Clear();
        }

        public void RemoveFromTabu(int allele)
        {
            _tabuGenes.Remove(allele);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Time Slot Index:  ");
            for (var i = 0; i < GeneLength; i++) stringBuilder.Append($"|{i}   ");

            stringBuilder.AppendLine();
            stringBuilder.Append("Student ID:       ");
            for (var i = 0; i < GeneLength; i++) stringBuilder.Append($"|{GetGeneValue(i)}   ");

            return stringBuilder.ToString();
        }
    }
}