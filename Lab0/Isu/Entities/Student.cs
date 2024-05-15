using Isu.Models;

namespace Isu.Entities
{
    public class Student
    {
        public Student(string name, Group group)
        {
            if (name == null)
            {
                throw new NullReferenceException("Name is null!");
            }

            if (group == null)
            {
                throw new NullReferenceException("Group is null!");
            }
            
            Id = int.Parse(GetHashCode().ToString()[..^2]);
            FullName = name;
            FirstName = name.Split(' ')[0];
            MiddleName = name.Split(' ')[1];
            LastName = name.Split(' ')[2];
            Group = group;
            CourseNumber = Group.CourseNumber;
            Faculty = GetFaculty(group.GroupName[0]);
        }


        public int Id { get; }
        public string FullName { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public Group Group { get; set; }
        public CourseNumber? CourseNumber { get; }
        public string Faculty { get; }
        
        private string GetFaculty(char code)
        {
            return code switch
            {
                'L' => "Институт лазерных технологий",
                'D' => "Институт международного развития и партнёрства",
                'O' => "Научно-образовательный центр инфохимии",
                'N' => "Факультет безопасности информационных технологий",
                'T' => "Факультет биотехнологий",
                'K' => "Факультет инфокоммуникабельных технологий",
                'M' => "Факультет информационных технологий и программирования",
                'P' => "Факультет программной инженерии и компьютерной техники",
                'R' => "Факультет систем управления и робототехники",
                'U' => "Факультет технологического менеджмента и инноваций",
                'V' => "Факультет фотоники",
                'G' => "Факультет экотехнологий",
                'Z' => "Физический факультет",
                'H' => "Центр химической инженерии",
                'W' => "Факультет энергетики и экотехнологий",
                'B' => "Центр прикладной оптики",
                _ => ""
            };
        }
    }
}