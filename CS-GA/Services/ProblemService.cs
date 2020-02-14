using CS_GA.Common.IData_Structure;
using CS_GA.Common.IProblems;
using CS_GA.Common.IServices;

namespace CS_GA.Services
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemDomain _problemDomain;

        public ProblemService(IProblemDomain problemDomain)
        {
            _problemDomain = problemDomain;
        }

        public bool ValidateSolution(IIndividual individual)
        {
            return _problemDomain.ValidateSolution(individual);
        }

        public void MakeSolutionValid(IIndividual individual)
        {
            _problemDomain.MakeSolutionValid(individual);
        }

        public int ConvertCsvDataToScore(string data)
        {
            return _problemDomain.ConvertCsvDataToScore(data);
        }
    }
}