using CS_GA.Common.IProblems;

namespace CS_GA.Common.IServices
{
    public interface IProblemService : IValidateIndividuals, IMakeSolutionValid, IConvertCsvDataToScore
    {
    }
}