using System;

namespace CS_GA.Common.IServices
{
    public interface IStudentDataService<out T>
    {
        int MaxNumberOfStudents { get; }
        int MaxNumberOfTimeSlots { get; }
        Type TypeOfData { get; }
        T GetStudentPreference(int studentIndex, int timeSlotIndex);
    }
}