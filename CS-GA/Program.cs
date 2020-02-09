using System;
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

            using (var reader = new StreamReader("F:\\repos\\CS-Machine-Intelligence-Algorithms\\Data\\StudentData.csv"))
            {
                if (ignoreHeader)
                    reader.ReadLine();

                int currentRowIndex = 0;
                while (!reader.EndOfStream)
                {
                    // New Line of Student Data
                    var basicFileLine = reader.ReadLine();

                    if (basicFileLine != null)
                    {
                        // Split the line by commands to retrieve a list of strings 
                        var itemsInFileLine = basicFileLine.Split(",");

                        // TODO: Change column index, and deal with the Student Name
                        for (int currentColumnIndex = 1; currentColumnIndex < itemsInFileLine.Length; currentColumnIndex++)
                        {
                            string preferenceAsLowerCase = itemsInFileLine[currentColumnIndex].ToLower();
                            int preferenceScore = ConvertStringDataToPreferenceScore(preferenceAsLowerCase);

                            //TODO: Change 'currentColumnIndex - 1'. It was done to get around the student name column causing an index out of bounds exception
                            studentPreferenceData.SetStudentPreference(currentRowIndex, currentColumnIndex - 1, preferenceScore);
                        }
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }

                    currentRowIndex++;
                }
            }

            var studentData = studentPreferenceData.GetStudentData(6).ToList();
            studentData.ForEach(data => Console.WriteLine(data));
            

            Console.ReadLine();
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
