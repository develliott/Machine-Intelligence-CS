using CS_GA.Business.Common.Data_Structure;

namespace CS_GA.Business.Common.Operators
{
    public interface IMutationOperator
    {
        IChromosome PerformMutation(IChromosome chromosome);
    }
}