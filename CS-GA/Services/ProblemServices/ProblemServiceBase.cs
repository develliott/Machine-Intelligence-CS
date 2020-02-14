using CS_GA.Common.IData_Structure;
using CS_GA.Common.IServices;

namespace CS_GA.Services.ProblemServices
{
    public abstract class ProblemServiceBase : IProblemService
    {
        public abstract bool ValidateSolution(IIndividual individual);

        public abstract int ConvertCsvDataToScore(string data);

        public abstract void MakeSolutionValid(IIndividual individual);
    }
}