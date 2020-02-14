using CS_GA.Common.IData_Structure;
using CS_GA.Common.IFactories;
using CS_GA.Common.IServices;

namespace CS_GA.Business.Data_Structure
{
    public class Population : IPopulation
    {
        private readonly IIndividualFactory _individualFactory;
        private readonly IProblemService _problemService;

        public Population(int size, IIndividualFactory individualFactory, IProblemService problemService)
        {
            Size = size;
            _individualFactory = individualFactory;
            _problemService = problemService;

            Individuals = new IIndividual[Size];
        }

        private IIndividual[] Individuals { get; }
        public IIndividual MostSuitableIndividualToProblem { get; set; }
        public int Size { get; }

        public void InitialisePopulation()
        {
            for (var individualIndex = 0; individualIndex < Size; individualIndex++)
            {
                var individual = _individualFactory.CreateIndividual();
                _problemService.MakeSolutionValid(individual);
                Individuals[individualIndex] = individual;
            }
        }

        public IIndividual GetIndividual(int populationIndex)
        {
            return Individuals[populationIndex];
        }

        public void SetIndividual(int populationIndex, IIndividual individual)
        {
            Individuals[populationIndex] = individual;
        }
    }
}