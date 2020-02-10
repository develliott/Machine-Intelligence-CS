using System;
using System.Collections.Generic;
using System.Text;

namespace CS_GA
{
    public interface IStudentPreferenceData<T>
    {
        T GetStudentPreference(int studentIndex, int timeslotIndex);
    }
}
