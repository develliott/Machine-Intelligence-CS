using System;
using System.Collections.Generic;
using System.Text;
using CS_GA.Business.Common;

namespace CS_GA.Business.Operators
{
    internal interface IMutationOperator
    {
        IChromosome PerformMutation(IChromosome chromosome);
    }
}