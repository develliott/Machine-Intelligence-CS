using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CS_GA.Genetic_Algorithm;
using Ninject;

namespace CS_GA
{
    class Program
    {
        private const string _filePath = "F:\\repos\\CS-Machine-Intelligence-Algorithms\\Data\\StudentData.csv";
        static void Main(string[] args)
        {
            CsvHelper<string> csvHelper = new CsvHelper<string>(_filePath, ",",true, true);
            StudentPreferenceData<int> studentPreferenceData = new StudentPreferenceData<int>(csvHelper.MaxRowIndex, csvHelper.MaxColumnIndex, csvHelper.GetCsvFileData(), ConvertStringDataToPreferenceScore);
            FitnessCalculator.SetStudentPreferenceData(studentPreferenceData);

            SecondMain secondMain = new SecondMain();
        }

        static int ConvertStringDataToPreferenceScore(string data)
        {
            int preferenceScore;
            switch (data.ToLower())
            {
                case "no":
                    preferenceScore = -10;
                    break;
                case "ok":
                    preferenceScore = 1;
                    break;
                case "pref":
                    preferenceScore = 10;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return preferenceScore;
        }
    }
}
