namespace CS_GA.Genetic_Algorithm
{
    public class Population
    {
        private readonly Individual[] _individuals;

        public Population(int populationSize, bool initialise)
        {
            PopulationSize = populationSize;
            _individuals = new Individual[PopulationSize];

            if (initialise) InitialisePopulation();
        }

        public int PopulationSize { get; }

        private void InitialisePopulation()
        {
            for (var individualIndex = 0; individualIndex < _individuals.Length; individualIndex++)
            {
                var newIndividual = new Individual();
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
            var fittest = _individuals[0];
            // Offset the index because we already have index 0 stored in the 'fittest' variable.
            var indexOffset = 1;

            for (var i = indexOffset; i < PopulationSize; i++)
                // If the current element has a higher fitness score than the current 'fittest' ->
                if (fittest.GetFitness() <= GetIndividual(i).GetFitness())
                    // -> assign it as the the current 'fittest'.
                    fittest = GetIndividual(i);
            return fittest;
        }

        public void SaveIndividual(int individualIndex, Individual individual)
        {
            _individuals[individualIndex] = individual;
        }
    }
}