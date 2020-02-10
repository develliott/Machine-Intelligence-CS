using System;
using System.Collections.Generic;
using System.Text;
using CS_GA.Data;
using CS_GA.Genetic_Algorithm;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace CS_GA.Ninject
{
    public class Bindings : NinjectModule
    {
        private const string _filePath = "F:\\repos\\CS-Machine-Intelligence-Algorithms\\Data\\StudentData.csv";

        public override void Load()
        {
            var csvHelper = Kernel.Get<CsvHelper<string>>(new ConstructorArgument("filePath", _filePath),
                new ConstructorArgument("delimiter", ","), new ConstructorArgument("ignoreFirstRow", true),
                new ConstructorArgument("ignoreFirstColumn", true));


            var studentPreferenceData = Kernel.Get<StudentPreferenceData<int>>(
                new ConstructorArgument("maxRowIndex", csvHelper.MaxRowIndex),
                new ConstructorArgument("maxColumnIndex", csvHelper.MaxColumnIndex), new ConstructorArgument("csvFileData", csvHelper.GetCsvFileData()));

            FitnessCalculator.SetStudentPreferenceData(studentPreferenceData);

            Kernel.Bind<IEvolutionService>().To<EvolutionService>();
        }
    }
}
