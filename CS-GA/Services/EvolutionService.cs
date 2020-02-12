using System;
using System.Collections.Generic;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;

namespace CS_GA.Services
{
    public class EvolutionService : IEvolutionService
    {
        private readonly bool _elitism = true;
        private readonly IEnvironmentService _environmentService;
        private readonly IIndividualFactory _individualFactory;
        private readonly int _outOfRangeValue = -1;
        private readonly IPopulationFactory _populationFactory;

        private readonly Random _random = new Random();
        private readonly IStudentDataService<int> _studentDataService;
        private readonly double mutationRate = 0.15;
        private readonly int tournamentSize = 5;
        private readonly double uniformRate = 0.25;

        public EvolutionService(IPopulationFactory populationFactory, IIndividualFactory individualFactory,
            IEnvironmentService environmentService, IStudentDataService<int> studentDataService)
        {
            _populationFactory = populationFactory;
            _individualFactory = individualFactory;
            _environmentService = environmentService;
            _studentDataService = studentDataService;
        }

        public void SetValidIndividual(IIndividual individual)
        {
            var tabuIndices = new List<int>();

            while (tabuIndices.Count < _studentDataService.MaxNumberOfStudents)
            {
                // Find a random index that hasn't been set yet.
                var randomIndex = _random.Next(_studentDataService.MaxNumberOfTimeslots);
                while (tabuIndices.Contains(randomIndex))
                {
                    randomIndex = _random.Next(_studentDataService.MaxNumberOfTimeslots);
                }
                tabuIndices.Add(randomIndex);


                var validAlleles = individual.GetValidAlleles();
                int newAllele;

                if (validAlleles.Count > 0)
                {
                    if (validAlleles.Count > 1)
                    {
                        var randomValidAlleleIndex = _random.Next(validAlleles.Count);
                        newAllele = validAlleles[randomValidAlleleIndex];
                    }
                    else
                    {
                        newAllele = validAlleles[0];
                    }

                    individual.SetGeneValue(randomIndex, newAllele);
                }
            }
        }

        public void SetBlankIndividual(IIndividual individual)
        {
            for (var geneIndex = 0; geneIndex < individual.GeneLength; geneIndex++)
            {
                individual.ClearTabuGenes();
                individual.SetGeneValue(geneIndex, _outOfRangeValue);
            }
        }

        public IPopulation EvolvePopulation(IPopulation population)
        {
            var newPopulation = _populationFactory.CreatePopulation(50);

            var individualIndexOffset = 0;
            if (_elitism)
            {
                newPopulation.SetIndividual(0, population.MostSuitableIndividualToProblem);
                individualIndexOffset = 1;
            }

            for (var i = individualIndexOffset; i < population.Size; i++)
            {
                var individual1 = tournamentSelection(population);

                var individual2 = tournamentSelection(population);

                var newIndividual = crossover(individual1, individual2);

                newPopulation.SetIndividual(i, newIndividual);
            }

            // Mutate
            for (var i = individualIndexOffset; i < population.Size; i++) mutate(newPopulation.GetIndividual(i));

            return newPopulation;
        }

        private IIndividual tournamentSelection(IPopulation population)
        {
            var tournamentPopulation = _populationFactory.CreatePopulation(5);

            for (var i = 0; i < tournamentPopulation.Size; i++)
            {
                var randomIndividualIndex = _random.Next(0, population.Size);
                tournamentPopulation.SetIndividual(i, population.GetIndividual(randomIndividualIndex));
            }

            _environmentService.UpdatePopulationSuitability(ref tournamentPopulation);

            return tournamentPopulation.MostSuitableIndividualToProblem;
        }

        private IIndividual crossover(IIndividual individual1, IIndividual individual2)
        {
            var newIndividual = _individualFactory.CreateIndividual();
            SetBlankIndividual(newIndividual);


            int crossoverIndex = _random.Next(newIndividual.GeneLength);

            for (int geneIndex = 0; geneIndex < newIndividual.GeneLength; geneIndex++)
            {
                int allele;

                if (geneIndex < crossoverIndex)
                {
                    allele = individual1.GetGeneValue(geneIndex);
                }
                else
                {
                    allele = individual2.GetGeneValue(geneIndex);
                }

                newIndividual.SetGeneValue(geneIndex, allele);

            }

            // TODO: Ensure new individual is valid.

            return newIndividual;

        }

        private void mutate(IIndividual individual)
        {
            //TODO: Check mutation results in a valid solution

            for (var geneIndex = 0; geneIndex < individual.GeneLength; geneIndex++)
            {
                // Mutate or skip
                if (_random.NextDouble() <= mutationRate)
                {
                    var randomAllele = _random.Next(_studentDataService.MaxNumberOfStudents);

                    individual.SetGeneValue(geneIndex, randomAllele);

                    individual.SwapAlleles(geneIndex, randomAllele);
                }
            }
        }
    }

    public interface IEvolutionService
    {
        void SetValidIndividual(IIndividual individual);
        void SetBlankIndividual(IIndividual individual);
        IPopulation EvolvePopulation(IPopulation population);
    }
}