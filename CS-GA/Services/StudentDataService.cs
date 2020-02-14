using System;
using System.Linq;
using CS_GA.Common.IServices;

namespace CS_GA.Services
{
    public class StudentDataService<T> : IStudentDataService<T>
    {
        private readonly dynamic[,] _csvFileData;
        private readonly IProblemService _problemService;

        // Knowledge Note: Rows then Columns for index size.
        private readonly T[,] _studentPreference;

        public StudentDataService(int maxRowIndex, int maxColumnIndex, dynamic[,] csvFileData,
            IProblemService problemService)
        {
            MaxNumberOfStudents = maxRowIndex;
            MaxNumberOfTimeSlots = maxColumnIndex;

            _csvFileData = csvFileData;
            _problemService = problemService;
            _studentPreference = new T[maxRowIndex, maxColumnIndex];

            TypeOfData = typeof(T);

            SetStudentPreference();
        }

        public int MaxNumberOfStudents { get; }
        public int MaxNumberOfTimeSlots { get; }

        public Type TypeOfData { get; }

        public T GetStudentPreference(int studentIndex, int timeSlotIndex)
        {
            return _studentPreference[studentIndex, timeSlotIndex];
        }

        private void SetStudentPreference()
        {
            for (var currentRowIndex = 0; currentRowIndex < MaxNumberOfStudents; currentRowIndex++)
            for (var currentColumnIndex = 0; currentColumnIndex < MaxNumberOfTimeSlots; currentColumnIndex++)
                _studentPreference[currentRowIndex, currentColumnIndex] =
                    _problemService.ConvertCsvDataToScore(_csvFileData[currentRowIndex, currentColumnIndex]);
        }

        public T[] GetStudentData(int studentIndex)
        {
            return Enumerable.Range(0, _studentPreference.GetLength(1))
                .Select(x => _studentPreference[studentIndex, x])
                .ToArray();
        }
    }
}