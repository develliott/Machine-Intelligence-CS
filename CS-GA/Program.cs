using System;
using CS_GA.Genetic_Algorithm;
using Ninject;
using Ninject.Parameters;

namespace CS_GA
{
    internal class Program
    {
        private const string _filePath = "F:\\repos\\CS-Machine-Intelligence-Algorithms\\Data\\StudentData.csv";

        private static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new Ninject.Bindings());


            var csvHelper = kernel.Get<CsvHelper<string>>(new ConstructorArgument("filePath", _filePath),
                new ConstructorArgument("delimiter", ","), new ConstructorArgument("ignoreFirstRow", true),
                new ConstructorArgument("ignoreFirstColumn", true));


            var studentPreferenceData = kernel.Get<StudentPreferenceData<int>>(
                new ConstructorArgument("maxRowIndex", csvHelper.MaxRowIndex),
                new ConstructorArgument("maxColumnIndex", csvHelper.MaxColumnIndex), new ConstructorArgument("csvFileData", csvHelper.GetCsvFileData()));
            
            FitnessCalculator.SetStudentPreferenceData(studentPreferenceData);

            var secondMain = new SecondMain();
        }

       
    }
}