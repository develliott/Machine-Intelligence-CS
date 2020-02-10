using System;
using System.Collections.Generic;
using System.Text;

namespace CS_GA.Data
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
