using System;
using System.Collections.Generic;
using System.Text;

namespace CS_GA.Data
{
    public class Individual<T>
    {
        private Chromosome<T> _chromosome;

        public Individual(Chromosome<T> chromosome)
        {
            _chromosome = chromosome;
        }
    }
}
