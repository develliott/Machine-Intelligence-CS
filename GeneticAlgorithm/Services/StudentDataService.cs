using System;
using System.Linq;

namespace CS_GA.Services
{
    public class StudentDataService<T> : IStudentDataService<T>
    {
        private readonly dynamic[,] _csvFileData;

        // Rows then Columns for index size.
        private readonly T[,] _studentPreference;
        private readonly int _maxColumnIndex;
        private readonly int _maxRowIndex;

        public StudentDataService(int maxRowIndex, int maxColumnIndex, dynamic[,] csvFileData)
        {
            _maxRowIndex = maxRowIndex;
            _maxColumnIndex = maxColumnIndex;
            MaxNumberOfStudents = _maxRowIndex;
            MaxNumberOfTimeslots = _maxColumnIndex;

            _csvFileData = csvFileData;
            _studentPreference = new T[maxRowIndex, maxColumnIndex];

            TypeOfData = typeof(T);

            SetStudentPreference();
        }

        public int MaxNumberOfStudents { get; }
        public int MaxNumberOfTimeslots { get; }

        public Type TypeOfData { get; }

        public T GetStudentPreference(int studentIndex, int timeslotIndex)
        {
            //TODO: Bug where -1 is still present in the chromosome and is used as Student Index, causing out of bounds exception.
            return _studentPreference[studentIndex, timeslotIndex];
        }

        private int ConvertStringDataToPreferenceScore(string data)
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

        private void SetStudentPreference()
        {
            for (var currentRowIndex = 0; currentRowIndex < MaxNumberOfStudents; currentRowIndex++)
            for (var currentColumnIndex = 0; currentColumnIndex < MaxNumberOfTimeslots; currentColumnIndex++)
                _studentPreference[currentRowIndex, currentColumnIndex] =
                    ConvertStringDataToPreferenceScore(_csvFileData[currentRowIndex, currentColumnIndex]);
        }

        public T[] GetStudentData(int studentIndex)
        {
            return Enumerable.Range(0, _studentPreference.GetLength(1))
                .Select(x => _studentPreference[studentIndex, x])
                .ToArray();
        }
    }

    public interface IStudentDataService<T>
    {
        int MaxNumberOfStudents { get; }
        int MaxNumberOfTimeslots { get; }
        Type TypeOfData { get; }
        T GetStudentPreference(int studentIndex, int timeslotIndex);
    }
}