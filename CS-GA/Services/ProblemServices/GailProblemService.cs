using System;
using System.Linq;
using CS_GA.Common.IData_Structure;

namespace CS_GA.Services.ProblemServices
{
    public class GailProblemService : ProblemServiceBase
    {
        private readonly int _maxNumberOfStudents;

        public GailProblemService(int maxNumberOfStudents)
        {
            _maxNumberOfStudents = maxNumberOfStudents;
        }

        public override bool ValidateSolution(IIndividual individual)
        {
            // Define a valid solution.
            //
            // 0 = Unassigned Time Slot
            // Each student must be assigned 1 time.

            var genes = individual.Chromosome.GetGenes().ToList();
            genes.RemoveAll(allele => allele.Equals(0));
            genes.Sort();
            var genesAsArray = genes.ToArray();

            var requiredAlleles = Enumerable.Range(1, _maxNumberOfStudents).ToArray();

            var validSolution = requiredAlleles.SequenceEqual(genesAsArray);

            return validSolution;
        }

        public override int ConvertCsvDataToScore(string data)
        {
            var preferenceScore = data.ToLower() switch
            {
                "no" => -50,
                "ok" => 1,
                "pref" => 10,
                _ => throw new InvalidOperationException()
            };

            return preferenceScore;
        }

        public override void MakeSolutionValid(IIndividual individual)
        {
            if (ValidateSolution(individual)) return;

            var chromosome = individual.Chromosome;
            var assignedAlleles = chromosome.GetAssignedAlleles();
            var allPossibleGenes = Enumerable.Range(1, _maxNumberOfStudents);
            var missingAlleles = allPossibleGenes.Except(assignedAlleles).ToList();

            while (missingAlleles.Any())
            {
                chromosome.SetRandomUnassignedAllele(0, missingAlleles[0]);
                missingAlleles.Remove(missingAlleles[0]);
            }


            // TODO: Move to a test
            if (!ValidateSolution(individual)) throw new InvalidOperationException("The solution is not valid.");
        }
    }
}