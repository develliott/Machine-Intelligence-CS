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

            using (var reader = new StreamReader("F:\\repos\\CS-Machine-Intelligence-Algorithms\\Data\\StudentData.csv"))
            {
                while (!reader.EndOfStream)
                {
                    if (ignoreHeader)
                        reader.ReadLine();

                    var basicFileLine = reader.ReadLine();

                    if (basicFileLine != null)
                    {
                        var itemsInFileLine = basicFileLine.Split(",").ToList();

                        itemsInFileLine.ForEach(element => Console.WriteLine(element));
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
