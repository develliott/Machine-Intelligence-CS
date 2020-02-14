using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS_GA.Common.IData_Structure;
using CS_GA.Common.IServices;

namespace CS_GA.Business.Data_Structure
{
    public class Individual : IIndividual
    {
        private readonly IStudentDataService<int> _studentDataService;

        private readonly List<int> _tabuGenes;


        public Individual(IStudentDataService<int> studentDataService)
        {
            _studentDataService = studentDataService;

            Chromosome = new Chromosome(studentDataService.MaxNumberOfTimeSlots);
            _tabuGenes = new List<int>();
        }

        public IChromosome Chromosome { get; }

        public int GeneLength => Chromosome.Size;

        public int SuitabilityScore { get; set; }

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

        public List<int> GetValidAlleles()
        {
            var allPossibleGenes = Enumerable.Range(1, _studentDataService.MaxNumberOfStudents);
            var validGenes = allPossibleGenes.Except(_tabuGenes);
            return validGenes.ToList();
        }

        public void ClearAllele(int allele)
        {
            for (var geneIndex = 0; geneIndex < GeneLength; geneIndex++)
                if (Chromosome.GetGeneValue(geneIndex) == allele)
                    Chromosome.SetGeneValue(geneIndex, -1);
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