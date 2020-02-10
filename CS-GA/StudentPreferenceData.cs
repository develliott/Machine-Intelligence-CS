using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_GA
{
    public class StudentPreferenceData<T> : IStudentPreferenceData<T>
    {
        private readonly int _maxRowIndex;
        private readonly int _maxColumnIndex;
        private readonly dynamic[,] _csvFileData;
        private readonly Func<string, T> _converterFunc;

        // Rows then Columns for index size.
        private readonly T[,] _studentPreference;

        public StudentPreferenceData(int maxRowIndex, int maxColumnIndex, dynamic[,] csvFileData, Func<string, T> converterFunc)
        {
            _maxRowIndex = maxRowIndex;
            _maxColumnIndex = maxColumnIndex;
            _csvFileData = csvFileData;
            _converterFunc = converterFunc;
            _studentPreference = new T[maxRowIndex, maxColumnIndex];

            SetStudentPreference();
        }

        private void SetStudentPreference()
        {
            for (var currentRowIndex = 0; currentRowIndex < _maxRowIndex; currentRowIndex++)
            {
                for (int currentColumnIndex = 0; currentColumnIndex < _maxColumnIndex; currentColumnIndex++)
                {
                    _studentPreference[currentRowIndex, currentColumnIndex] =
                        _converterFunc(_csvFileData[currentRowIndex, currentColumnIndex]);
                }
            }
        }

        public T GetStudentPreference(int studentIndex, int timeslotIndex)
        {
            //TODO: Bug where -1 is still present in the chromosome and is used as Student Index, causing out of bounds exception.
            return _studentPreference[studentIndex, timeslotIndex];
        }

        public T[] GetStudentData(int studentIndex)
        {
            return Enumerable.Range(0, _studentPreference.GetLength(1))
                .Select(x => _studentPreference[studentIndex, x])
                .ToArray();
        }
    }
}
