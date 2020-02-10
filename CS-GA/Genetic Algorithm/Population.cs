using System;
using System.Collections.Generic;
using System.Text;

namespace CS_GA.Genetic_Algorithm
{
    public class Population
    {
        private Individual[] _individuals;

        public int PopulationSize { get; }

        public Population(int populationSize, bool initialise)
        {
            PopulationSize = populationSize;
            _individuals = new Individual[PopulationSize];

            if (initialise)
            {
                InitialisePopulation();
            }
        }

        private void InitialisePopulation()
        {
            for (var individualIndex = 0; individualIndex < _individuals.Length; individualIndex++)
            {
                Individual newIndividual = new Individual();
                _individuals[individualIndex] = newIndividual;
            }
        }

        public Individual GetIndividual(int index)
        {
            return _individuals[index];
        }

        //TODO: Get fittest method

        public Individual GetFittestIndividual()
        {
            Individual fittest = _individuals[0];
            // Offset the index because we already have index 0 stored in the 'fittest' variable.
            int indexOffset = 1;

            for (int i = indexOffset; i < PopulationSize; i++)
            {
                // If the current element has a higher fitness score than the current 'fittest' ->
                if (fittest.GetFitness() <= GetIndividual(i).GetFitness())
                {
                    // -> assign it as the the current 'fittest'.
                    fittest = GetIndividual(i);
                }
            }
            return fittest;
        }

        public void SaveIndividual(int individualIndex, Individual individual)
        {
            _individuals[individualIndex] = individual;
        }

    }
}
