using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CS_GA
{
    public class CsvHelper<T>
    {
        private string _filePath;
        private bool _ignoreFirstRow;
        private bool _ignoreFirstColumn;
        private readonly string _delimiter;

        private T[,] _csvFileData;

        public int MaxRowIndex { get; private set; }
        public int MaxColumnIndex { get; private set; }

        public CsvHelper(string filePath, string delimiter, bool ignoreFirstRow = false, bool ignoreFirstColumn = false)
        {
            _filePath = filePath;
            _delimiter = delimiter;
            _ignoreFirstRow = ignoreFirstRow;
            _ignoreFirstColumn = ignoreFirstColumn;

            _csvFileData = GetCsvFileDataDimensions();
        }

        /// <summary>
        /// Scans the file for the number of Rows and Columns.
        /// </summary>
        /// <returns>Two dimensional array with the size matching the data's Rows and Columns requirements.</returns>
        public T[,] GetCsvFileDataDimensions()
        {
            int maxRowIndex = 0;
            int maxColumnIndex = 0;

            using (var fileReader = new StreamReader(_filePath) )
            {
                if (_ignoreFirstRow)
                    // Move the reader onto the next line of the file.
                    fileReader.ReadLine();

                while (!fileReader.EndOfStream)
                {
                    var fileLine = fileReader.ReadLine();

                    // Only perform calculation once.
                    if (maxRowIndex == 0)
                    {
                        // Apply an offset of 1 if we are too ignore the first column.
                        int offset = _ignoreFirstColumn ? 1 : 0;
                        maxColumnIndex = fileLine.Split(_delimiter).Length - offset;
                    }

                    maxRowIndex++;
                }
            }

            MaxRowIndex = maxRowIndex;
            MaxColumnIndex = maxColumnIndex;
            return new T[maxRowIndex, maxColumnIndex];
        }
    }
}
