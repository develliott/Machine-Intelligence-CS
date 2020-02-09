using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_GA
{
    public class StudentPreferenceData
    {
        // Rows then Columns for index size.
        private readonly int[,] _studentPreference;

        public StudentPreferenceData(int[,] studentPreference)
        {
            _studentPreference = studentPreference;
        }

        public void SetStudentPreference(int studentIndex, int timeslotIndex, int preferenceScore )
        {
            _studentPreference[studentIndex, timeslotIndex] = preferenceScore;
        }

        public int GetStudentPreference(int studentIndex, int timeslotIndex)
        {
            return _studentPreference[studentIndex, timeslotIndex];
        }

        public int[] GetStudentData(int studentIndex)
        {
            return Enumerable.Range(0, _studentPreference.GetLength(1))
                .Select(x => _studentPreference[studentIndex, x])
                .ToArray();
        }
    }
}
