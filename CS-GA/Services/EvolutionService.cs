using CS_GA.Common.IData_Structure;
using CS_GA.Common.IFactories;
using CS_GA.Common.IOperators;
using CS_GA.Common.IServices;

namespace CS_GA.Services
{
    public class EvolutionService : IEvolutionService
    {
        private readonly ICrossoverOperator _crossoverOperator;
        private readonly bool _elitism = true;
        private readonly IMutationOperator _mutationOperator;
        private readonly IPopulationFactory _populationFactory;
        private readonly IProblemService _problemService;

        public EvolutionService(IProblemService problemService, IPopulationFactory populationFactory,
            ICrossoverOperator crossoverOperator,
            IMutationOperator mutationOperator)
        {
            _problemService = problemService;
            _populationFactory = populationFactory;
            _crossoverOperator = crossoverOperator;
            _mutationOperator = mutationOperator;
        }

        public IPopulation EvolvePopulation(IPopulation oldPopulation)
        {
            var newPopulation = _populationFactory.CreatePopulation(oldPopulation.Size);

            var individualIndexOffset = 0;
            if (_elitism)
            {
                newPopulation.SetIndividual(0, oldPopulation.MostSuitableIndividualToProblem);
                individualIndexOffset = 1;
            }

            // Populate 'newPopulation' with children from the parents in 'oldPopulation' to exploit the current knowledge.
            for (var individualIndex = individualIndexOffset; individualIndex < newPopulation.Size; individualIndex++)
            {
                var resultingIndividualFromCrossoverOperation =
                    _crossoverOperator.PerformCrossover(oldPopulation);

                _problemService.MakeSolutionValid(resultingIndividualFromCrossoverOperation);

                newPopulation.SetIndividual(individualIndex, resultingIndividualFromCrossoverOperation);
            }

            // Mutate each individual in the new population to explore the problem domain.
            for (var individualIndex = individualIndexOffset; individualIndex < oldPopulation.Size; individualIndex++)
            {
                // TODO: Refactor the 'currentIndividual.Chromosome' - shouldn't be able to view Chromosome.
                var currentIndividual = newPopulation.GetIndividual(individualIndex);
                _mutationOperator.PerformMutation(currentIndividual.Chromosome);
            }

            return newPopulation;
        }
    }
}