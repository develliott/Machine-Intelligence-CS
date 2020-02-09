using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CS_GA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            bool ignoreHeader = true;

            // TODO: Dynamically set data array length
            StudentPreferenceData studentPreferenceData = new StudentPreferenceData(new int[7,14]);

            Dictionary<string, int> _studentNameToIndexMapping = new Dictionary<string, int>();

            using (var reader = new StreamReader("F:\\repos\\CS-Machine-Intelligence-Algorithms\\Data\\StudentData.csv"))
            {
                int currentRowIndex = 0;

                if (ignoreHeader)
                    reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    // New Line of Student Data
                    var basicFileLine = reader.ReadLine();

                    if (basicFileLine != null)
                    {
                        // Split the line by commands to retrieve a list of strings 
                        var itemsInFileLine = basicFileLine.Split(",").ToList();

                        // Read the first element as Student Name and then remove it
                        _studentNameToIndexMapping.Add(itemsInFileLine[0], currentRowIndex);
                        itemsInFileLine.Remove(itemsInFileLine[0]);

                        for (int currentColumnIndex = 0; currentColumnIndex < itemsInFileLine.Count; currentColumnIndex++)
                        {
                            string preferenceAsLowerCase = itemsInFileLine[currentColumnIndex].ToLower();
                            int preferenceScore = ConvertStringDataToPreferenceScore(preferenceAsLowerCase);

                            studentPreferenceData.SetStudentPreference(currentRowIndex, currentColumnIndex, preferenceScore);
                        }
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }

                    currentRowIndex++;
                }
            }
        }

        static int ConvertStringDataToPreferenceScore(string data)
        {
            int preferenceScore;
            switch (data)
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
