using System;
using System.Collections.Generic;
using System.Text;

namespace CS_GA.Genetic_Algorithm
{
    public class Population
    {
        private Individual[] _individuals;

        public Population(int populationSize, bool initialise)
        {
            _individuals = new Individual[populationSize];

            if (initialise)
            {
                InitialisePopulation();
            }
        }

        private void InitialisePopulation()
        {
            for (var individualIndex = 0; individualIndex < _individuals.Length; individualIndex++)
            {
                Individual newIndividual = new Individual(0,0);
                _individuals[individualIndex] = newIndividual;
            }
        }

        public Individual GetIndividual(int index)
        {
            return _individuals[index];
        }

        //TODO: Get fittest method

    }
}
