﻿using System;
using System.Collections.Generic;
using System.Text;
using CS_GA.Business.Common;

namespace CS_GA.Business.Operators
{
    public interface ICrossoverOperator
    {
        IIndividual PerformCrossover(IIndividual newIndividual, IIndividual individual1, IIndividual individual2);
    }
}
