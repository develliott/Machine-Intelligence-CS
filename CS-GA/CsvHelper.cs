using System;
using System.IO;
using System.Linq;

namespace CS_GA
{
    public class CsvHelper<T>
    {
        private readonly string _delimiter;

        private readonly T[,] _csvFileData;
        private readonly string _filePath;
        private readonly bool _ignoreFirstColumn;
        private readonly bool _ignoreFirstRow;

        public int MaxRowIndex { get; private set; }
        public int MaxColumnIndex { get; private set; }

        public CsvHelper(string filePath, string delimiter, bool ignoreFirstRow = false, bool ignoreFirstColumn = false)
        {
            _filePath = filePath;
            _delimiter = delimiter;
            _ignoreFirstRow = ignoreFirstRow;
            _ignoreFirstColumn = ignoreFirstColumn;

            _csvFileData = GetCsvFileDataDimensions();

            SetCsvFileData();
        }

        public T[,] GetCsvFileData()
        {
            return _csvFileData;
        }

        /// <summary>
        /// Populates the two dimensional array with the CSV file values.
        /// </summary>
        private void SetCsvFileData()
        {
            using (var fileReader = new StreamReader(_filePath))
            {
                var currentRowIndex = 0;

                if (_ignoreFirstRow)
                    // Move the reader onto the next line of the file.
                    fileReader.ReadLine();

                while (!fileReader.EndOfStream)
                {
                    var fileLine = fileReader.ReadLine() ?? throw new ArgumentNullException("fileReader.ReadLine()");

                    var offset = _ignoreFirstColumn ? 1 : 0;
                    var fileLineElements = fileLine.Split(_delimiter).Skip(offset).ToArray();

                    for (var currentColumnIndex = 0;
                        currentColumnIndex < fileLineElements.Count();
                        currentColumnIndex++)
                        _csvFileData[currentRowIndex, currentColumnIndex] =
                            (T) (object) fileLineElements[currentColumnIndex];

                    currentRowIndex++;
                }
            }
        }

        public T GetValue(int rowIndex, int columnIndex)
        {
            return _csvFileData[rowIndex, columnIndex];
        }

        /// <summary>
        ///     Scans the file for the number of Rows and Columns.
        /// </summary>
        /// <returns>Two dimensional array with the size matching the data's Rows and Columns requirements.</returns>
        private T[,] GetCsvFileDataDimensions()
        {
            var maxRowIndex = 0;
            var maxColumnIndex = 0;

            using (var fileReader = new StreamReader(_filePath))
            {
                if (_ignoreFirstRow)
                    // Move the reader onto the next line of the file.
                    fileReader.ReadLine();

                while (!fileReader.EndOfStream)
                {
                    var fileLine = fileReader.ReadLine() ?? throw new ArgumentNullException("fileReader.ReadLine()");

                    // Only perform calculation once.
                    if (maxRowIndex == 0)
                    {
                        // Apply an offset of 1 if we are too ignore the first column.
                        var offset = _ignoreFirstColumn ? 1 : 0;
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