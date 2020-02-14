using CS_GA.Business.Data_Structure;
using CS_GA.Business.Operators;
using CS_GA.Business.Strategies;
using CS_GA.Common.IData_Structure;
using CS_GA.Common.IFactories;
using CS_GA.Common.IOperators;
using CS_GA.Common.IServices;
using CS_GA.Common.IStrategies;
using CS_GA.DAL;
using CS_GA.Services;
using CS_GA.Services.ProblemServices;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Parameters;

namespace CS_GA.Ninject
{
    public class Bindings : NinjectModule
    {
        private const string _filePath = "F:\\repos\\CS-Machine-Intelligence-Algorithms\\Data\\StudentData.csv";
        // private const string _filePath = "C:\\dev\\GitHub\\Machine-Intelligence-CS\\Data\\StudentData.csv";

        public override void Load()
        {
            var csvHelper = Kernel.Get<CsvHelper<string>>(new ConstructorArgument("filePath", _filePath),
                new ConstructorArgument("delimiter", ","), new ConstructorArgument("ignoreFirstRow", true),
                new ConstructorArgument("ignoreFirstColumn", true));


            // TODO: Clean up 'WithConstructorArgument' statements.
            // Services
            Kernel.Bind<IEvolutionService>().To<EvolutionService>().InSingletonScope();
            Kernel.Bind<IStudentDataService<int>>().To<StudentDataService<int>>().InSingletonScope()
                .WithConstructorArgument("maxRowIndex", csvHelper.MaxRowIndex)
                .WithConstructorArgument("maxColumnIndex", csvHelper.MaxColumnIndex)
                .WithConstructorArgument("csvFileData", csvHelper.GetCsvFileData());
            Kernel.Bind<IEnvironmentService>().To<EnvironmentService>().InSingletonScope();

            Kernel.Bind<IProblemService>().To<GailProblemService>().InSingletonScope()
                .WithConstructorArgument("maxNumberOfStudents", csvHelper.MaxRowIndex);

            // Define Strategies and Operators
            Kernel.Bind<ISelectionStrategy>().To<TournamentSelection>().InSingletonScope();
            Kernel.Bind<ICrossoverOperator>().To<OnePointCrossoverOperator>().InSingletonScope();
            Kernel.Bind<IMutationOperator>().To<SwapMutationOperator>().InSingletonScope();

            // Transient Scoped Bindings 
            Kernel.Bind<IIndividual>().To<Individual>();
            Kernel.Bind<IPopulation>().To<Population>();


            // Factories
            Kernel.Bind<IIndividualFactory>().ToFactory();
            Kernel.Bind<IPopulationFactory>().ToFactory();
        }
    }
}