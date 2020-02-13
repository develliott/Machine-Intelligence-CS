using System;
using System.Linq;

namespace CS_GA.Services
{
    public class StudentDataService<T> : IStudentDataService<T>
    {
        private readonly dynamic[,] _csvFileData;

        // Knowledge Note: Rows then Columns for index size.
        private readonly T[,] _studentPreference;

        public StudentDataService(int maxRowIndex, int maxColumnIndex, dynamic[,] csvFileData)
        {
            MaxNumberOfStudents = maxRowIndex;
            MaxNumberOfTimeSlots = maxColumnIndex;

            _csvFileData = csvFileData;
            _studentPreference = new T[maxRowIndex, maxColumnIndex];

            TypeOfData = typeof(T);

            SetStudentPreference();
        }

        public int MaxNumberOfStudents { get; }
        public int MaxNumberOfTimeSlots { get; }

        public Type TypeOfData { get; }


        private int ConvertStringDataToPreferenceScore(string data)
        {
            // TODO: Refactor into environment service
            // - think about where it belongs more.
            int preferenceScore;
            switch (data.ToLower())
            {
                case "no":
                    preferenceScore = -50;
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
            for (var currentColumnIndex = 0; currentColumnIndex < MaxNumberOfTimeSlots; currentColumnIndex++)
                _studentPreference[currentRowIndex, currentColumnIndex] =
                    ConvertStringDataToPreferenceScore(_csvFileData[currentRowIndex, currentColumnIndex]);
        }

        public T[] GetStudentData(int studentIndex)
        {
            return Enumerable.Range(0, _studentPreference.GetLength(1))
                .Select(x => _studentPreference[studentIndex, x])
                .ToArray();
        }

        public T GetStudentPreference(int studentIndex, int timeSlotIndex)
        {
            return _studentPreference[studentIndex, timeSlotIndex];
        }
    }

    public interface IStudentDataService<out T>
    {
        int MaxNumberOfStudents { get; }
        int MaxNumberOfTimeSlots { get; }
        Type TypeOfData { get; }
        T GetStudentPreference(int studentIndex, int timeSlotIndex);
    }
}