using System;
using System.Linq;
using CS_GA.Business.Data_Structure;
using CS_GA.Common.IData_Structure;
using CS_GA.Common.IProblems;
using CS_GA.Common.IServices;
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

        public bool ValidateSolution(IIndividual individual)
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

        public void MakeSolutionValid(IIndividual individual)
        {
            if (ValidateSolution(individual))
            {
                return;
            }

            var chromosome = individual.Chromosome;
            var assignedAlleles = chromosome.GetAssignedAlleles();
            var allPossibleGenes = Enumerable.Range(1, _studentDataService.MaxNumberOfStudents);
            var missingAlleles = allPossibleGenes.Except(assignedAlleles).ToList();

            while (missingAlleles.Any())
            {
                chromosome.SetRandomUnassignedAllele(0, missingAlleles[0]);
                missingAlleles.Remove(missingAlleles[0]);
            }
            

            // TODO: Move to a test
            if (!ValidateSolution(individual))
            {
                throw new InvalidOperationException("The solution is not valid.");
            }
        }
    }
}
