using System;
using System.Collections.Generic;
using System.Text;
using CS_GA.Business.Common;

namespace CS_GA.Business.Strategies
{
    public interface ISelectionStrategy
    {
        IIndividual SelectIndividualFromPopulation(IPopulation population);
    }
}
