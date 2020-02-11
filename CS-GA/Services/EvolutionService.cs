using System;
using System.Collections.Generic;
using CS_GA.Business.Common;
using CS_GA.Business.Common.Factories;
using CS_GA.Business.GA_Data_Structure;

namespace CS_GA.Services
{
    public class EvolutionService : IEvolutionService
    {
        private readonly IPopulationFactory _populationFactory;
        private readonly IIndividualFactory _individualFactory;
        private readonly IEnvironmentService _environmentService;
        private readonly IStudentDataService<int> _studentDataService;

        private readonly bool _elitism = true;
        private readonly double mutationRate = 0.15;
        private readonly int tournamentSize = 5;
        private readonly double uniformRate = 0.5;

        private readonly Random _random = new Random();
        private readonly int _outOfRangeValue = -1;

        public EvolutionService(IPopulationFactory populationFactory, IIndividualFactory individualFactory, IEnvironmentService environmentService, IStudentDataService<int> studentDataService)
        {
            _populationFactory = populationFactory;
            _individualFactory = individualFactory;
            _environmentService = environmentService;
            _studentDataService = studentDataService;
        }

        public void SetValidIndividual(IIndividual individual)
        {
            for (var geneIndex = 0; geneIndex < individual.GeneLength; geneIndex++)
            {
                List<int> validGenes = individual.GetValidGenes();

                if (validGenes.Count > 0)
                {
                    int randomValidGeneIndex = _random.Next(validGenes.Count);
                    int randomValidGene = validGenes[randomValidGeneIndex];
                    individual.SetGeneValue(geneIndex, randomValidGene);
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
            IPopulation newPopulation = _populationFactory.CreatePopulation(50);

            int individualIndexOffset = 0;
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
            IPopulation tournamentPopulation = _populationFactory.CreatePopulation(50);

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
            IIndividual newIndividual = _individualFactory.CreateIndividual();
            
            for (var geneIndex = 0; geneIndex < newIndividual.GeneLength; geneIndex++)
            {
                int newAllele;
                List<int> newIndividualValidGenes = newIndividual.GetValidGenes();

                var individual1AlleleAtCurrentIndex = individual1.GetGeneValue(geneIndex);
                var individual2AlleleAtCurrentIndex = individual2.GetGeneValue(geneIndex);

                var individual1Valid = newIndividualValidGenes.Contains(individual1AlleleAtCurrentIndex);
                var individual2Valid = newIndividualValidGenes.Contains(individual2AlleleAtCurrentIndex);

                if(individual1Valid && individual2Valid)
                {
                    if (_random.NextDouble() <= uniformRate)
                        newAllele = individual1.GetGeneValue(geneIndex);
                    else
                        newAllele = individual2.GetGeneValue(geneIndex);
                }
                else if (!individual1Valid && !individual2Valid)
                {
                    int randomAllele = _random.Next(_studentDataService.MaxNumberOfStudents);

                    if (newIndividual.IsGeneAlreadyAssigned(randomAllele))
                    {
                        newIndividual.ClearAllele(randomAllele);
                    }

                    newAllele = randomAllele;
                }
                else if (individual1Valid)
                {
                    newAllele = individual1.GetGeneValue(geneIndex);
                }
                else
                {
                    newAllele = individual2.GetGeneValue(geneIndex);

                }

                newIndividual.SetGeneValue(geneIndex, newAllele);
            }

            return newIndividual;
        }

        private void mutate(IIndividual individual)
        {
            for (int geneIndex = 0; geneIndex < individual.GeneLength; geneIndex++)
            {
                // Mutate or skip
                if (_random.NextDouble() <= mutationRate)
                {
                    var validGenes = individual.GetValidGenes();
                    var randomAllele = _random.Next(validGenes.Count);

                    individual.SetGeneValue(geneIndex, randomAllele);
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