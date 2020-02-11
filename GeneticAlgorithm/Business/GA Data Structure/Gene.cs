namespace CS_GA.Business.GA_Data_Structure
{
    public class Gene<T>
    {
        public T Allele { get; private set; }

        public void SetAllele(T value)
        {
            Allele = value;
        }
    }
}