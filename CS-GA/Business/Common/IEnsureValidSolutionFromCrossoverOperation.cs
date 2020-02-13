using CS_GA.Business.Common.Data_Structure;

namespace CS_GA.Business.Common
{
    public interface IEnsureValidSolutionFromCrossoverOperation
    {
        void MakeSolutionValid(IIndividual individual);
    }
}
