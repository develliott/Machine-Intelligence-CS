using System;
using System.Linq;
using CS_GA.Common.IData_Structure;
using CS_GA.Common.IProblems;

namespace CS_GA.Business.Problems
{
    public class GailProblemDomain : IProblemDomain
    {
        private readonly int _maxNumberOfStudents;

        public GailProblemDomain(int maxNumberOfStudents)
        {
            _maxNumberOfStudents = maxNumberOfStudents;
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

            var requiredAlleles = Enumerable.Range(1, _maxNumberOfStudents).ToArray();

            var validSolution = requiredAlleles.SequenceEqual(genesAsArray);

            return validSolution;
        }

        public int ConvertCsvDataToScore(string data)
        {
            int preferenceScore;
            switch (data.ToLower())
            {
                case "no":
                    preferenceScore = -50;
                    break;
                case "ok":
                    preferenceScore = 1;
                    break;
                case "pref":
                    preferenceScore = 10;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return preferenceScore;
        }

        public void MakeSolutionValid(IIndividual individual)
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