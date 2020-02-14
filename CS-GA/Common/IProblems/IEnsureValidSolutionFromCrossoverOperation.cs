using CS_GA.Common.IData_Structure;

namespace CS_GA.Common.IProblems
{
    public interface IEnsureValidSolutionFromCrossoverOperation
    {
        void MakeSolutionValid(IIndividual individual);
    }
}