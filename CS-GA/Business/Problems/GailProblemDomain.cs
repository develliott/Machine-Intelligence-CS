using System.Linq;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Data_Structure;
using CS_GA.Services;

namespace CS_GA.Business.Problems
{
    public class GailProblemDomain : IProblemDomain
    {
        private readonly IStudentDataService<int> _studentDataService;

        public GailProblemDomain(IStudentDataService<int> studentDataService)
        {
            _studentDataService = studentDataService;
        }

        public bool ValidateIndividual(IIndividual individual)
        {
            // Define a valid solution.
            //
            // 0 = Unassigned Time Slot
            // Each student must be assigned 1 time.

            var genes = individual.Chromosome.GetGenes().ToList();
            genes.RemoveAll(allele => allele.Equals(0));
            genes.Sort();
            var genesAsArray = genes.ToArray();

            var numberOfStudents = _studentDataService.MaxNumberOfStudents;
            var requiredAlleles = Enumerable.Range(1, numberOfStudents).ToArray();

            var validSolution = requiredAlleles.SequenceEqual(genesAsArray);

            return validSolution;
        }
    }
}
