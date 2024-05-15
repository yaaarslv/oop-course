using Isu.Entities;
using Isu.Services;
using Isu.Tools;

namespace Isu.Models
{
    public class CourseNumber
    {
        private const int MinCourseNumber = 1;
        private const int MaxCourseNumber = 4;
        
        public CourseNumber(int number)
        {
            if (!CheckCourseNumber(number))
            {
                throw new InvalidCourseNumberException($"Coursenumber {number} is invalid!");
            }

            Number = number;
        }
        
        public int Number { get; }
        private bool CheckCourseNumber(int number)
        {
            return number >= MinCourseNumber && number <= MaxCourseNumber;
        }
    }
}