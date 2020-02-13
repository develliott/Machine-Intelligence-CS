namespace CS_GA.Business.Data_Structure.Helpers
{
    public class ChromosomeConstraint : IChromosomeConstraint
    {
        public ChromosomeConstraint(int maxGeneValue)
        {
            MaxGeneValue = maxGeneValue;
        }

        public int MaxGeneValue { get; }
    }

    public interface IChromosomeConstraint
    {
        int MaxGeneValue { get; }
    }
}