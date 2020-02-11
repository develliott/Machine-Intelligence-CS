using System;
using System.Linq;
using System.Text;
using CS_GA.Services;

namespace CS_GA.Genetic_Algorithm
{
    public class Individual
    {
        private readonly IEnvironmentService _environmentService;
        private readonly int _maxGeneValue;
        private readonly int[] _chromosome;

        public Individual()
        {
            ChromosomeSize = 14;
            _chromosome = new int[ChromosomeSize];
            InitialiseChromosome();
        }

        public Individual(IStudentDataService<int> studentDataService, IEnvironmentService environmentService)
        {
            _environmentService = environmentService;
            // _maxGeneValue = studentDataService.MaxRowIndex;
            // ChromosomeSize = studentDataService.MaxColumnIndex;
            _chromosome = new int[ChromosomeSize];

            InitialiseChromosome();
            GenerateIndividual();
        }

        public int ChromosomeSize { get; }

        /// <summary>
        ///     Initialise the chromosome so it can be successfully used by other parts of the code.
        /// </summary>
        private void InitialiseChromosome()
        {
            for (var geneIndex = 0; geneIndex < _chromosome.Length; geneIndex++)
                // The default value for 'new int[]' is full of 0's.
                // However, 0 is a valid index value.
                // So, set the chromosome's values out of range for initialisation.
                _chromosome[geneIndex] = -1;
        }

        /// <summary>
        ///     Format the Individual to show index and gene value.
        /// </summary>
        /// <returns>An overview of the current chromosome data.</returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Index:   ");
            for (var geneIndex = 0; geneIndex < _chromosome.Length; geneIndex++) stringBuilder.Append($"{geneIndex}  ");

            stringBuilder.AppendLine();
            stringBuilder.Append("Allele:  ");

            for (var geneIndex = 0; geneIndex < _chromosome.Length; geneIndex++)
                stringBuilder.Append($"{_chromosome[geneIndex]}  ");

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Generate a valid candidate solution.
        /// </summary>
        private void GenerateIndividual()
        {
            for (var geneIndex = 0; geneIndex < _maxGeneValue; geneIndex++)
            {
                var gene = GenerateNewGene();
                //TODO: Random assign (not linear index)
                _chromosome[geneIndex] = gene;
            }
        }

        /// <summary>
        ///     Find a gene that hasn't already been assigned in the chromosome;
        /// </summary>
        /// <returns>A valid gene for the chromosome.</returns>
        public int GenerateNewGene()
        {
            var r = new Random();
            var newGene = r.Next(_maxGeneValue);

            if (GeneAlreadyAssigned(newGene)) newGene = GenerateNewGene();

            return newGene;
        }

        /// <summary>
        ///     Determines if a gene is already present in the chromosome.
        /// </summary>
        /// <param name="newGene">The gene to test for.</param>
        /// <returns>Bool representing if the gene is already present.</returns>
        public bool GeneAlreadyAssigned(int newGene)
        {
            return _chromosome.ToList().Exists(gene => gene == newGene);
        }

        public int GetAllele(int geneIndex)
        {
            return _chromosome[geneIndex];
        }

        public int GetFitness()
        {
            // return _environmentService.UpdateIndividualSuitability(this);
            return 0;
        }

        public void SetAllele(int geneIndex, int geneAllele)
        {
            _chromosome[geneIndex] = geneAllele;
        }
    }
}