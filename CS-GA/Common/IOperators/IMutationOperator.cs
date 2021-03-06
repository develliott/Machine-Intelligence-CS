﻿using CS_GA.Common.IData_Structure;

namespace CS_GA.Common.IOperators
{
    public interface IMutationOperator
    {
        IIndividual PerformMutation(IIndividual individual);
    }
}