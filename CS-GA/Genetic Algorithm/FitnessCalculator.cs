using System;
using System.Collections.Generic;
using System.Text;

namespace CS_GA.Genetic_Algorithm
{
    public static class FitnessCalculator
    {
        private static StudentPreferenceData<int> _studentPreferenceData;

        public static void SetStudentPreferenceData(StudentPreferenceData<int> studentPreferenceData) =>
            _studentPreferenceData = studentPreferenceData;

        public static int GetIndividualFitness(Individual individual)
        {
            int fitness = 0;

            for (int i = 0; i < individual.ChromosomeSize; i++)
            {
                int studentIndex = individual.GetAllele(i);
                int timeslotIndex = i;

                fitness += _studentPreferenceData.GetStudentPreference(studentIndex, timeslotIndex);
            }

            return fitness;
        }

    }
}
