using CS_GA.Business.Common.Data_Structure;

namespace CS_GA.Business.Common
{
    public interface IValidateIndividuals
    {
        bool ValidateSolution(IIndividual individual);
    }
}
