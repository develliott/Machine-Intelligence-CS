using CS_GA.Common.IData_Structure;

namespace CS_GA.Common.IProblems
{
    public interface IValidateIndividuals
    {
        bool ValidateSolution(IIndividual individual);
    }
}
