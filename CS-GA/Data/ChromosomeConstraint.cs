using System;
using System.Collections.Generic;
using System.Text;

namespace CS_GA.Data
{
    public class ChromosomeConstraint
    {
        public int MaxGeneValue { get; }

        public ChromosomeConstraint(int maxGeneValue)
        {
            MaxGeneValue = maxGeneValue;
        }
    }
}
