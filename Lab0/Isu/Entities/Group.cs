using Isu.Models;
using Isu.Services;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        public const int GroupCapacity = 25;
        private const int _minGroupNameLength = 5;
        private const int _maxGroupNameLength = 6;
        private readonly List<Student> _students;
        
        public Group(string name)
        {
            if (name == null)
            {
                throw new NullReferenceException("Groupname is null!");
            }
            
            CheckGroupname(name);
            
            GroupName = name;
            CourseNumber = new CourseNumber(Convert.ToInt32(GroupName[2].ToString()));
            _students = new List<Student>();
        }
        
        public string GroupName { get; }
        public CourseNumber CourseNumber { get; }
        public IReadOnlyCollection<Student> Students => _students;
        
        private void CheckGroupname(string name)
        {
            if (name.Length < _minGroupNameLength || name.Length > _maxGroupNameLength || !Char.IsLetter(name[0]) || !name[1..].All(c => Char.IsNumber(c)))
            {
                throw new InvalidGroupNameException($"Groupname {name} is invalid!");
            }
        }

        public void CheckGroupCapacity()
        {
            if (Students.Count >= GroupCapacity)
            {
                throw new GroupIsFullException($"Can't add student to a group {GroupName} because group is full!");
            }
        }
        
        public void AddStudent(Student student)
        {
            CheckGroupCapacity();
            if (_students.Contains(student))
            {
                throw new StudentAlreadyExistsException($"Can't add student to a group {GroupName} because he is already exists in group {student.Group.GroupName}");
            }

            _students.Add(student);
            student.Group = this;
        }

        public void RemoveStudent(Student student)
        {
            if (!_students.Contains(student))
            {
                throw new StudentNotExistsException($"Can't pop student because he doesn't exist in group {GroupName}");
            }

            _students.Remove(student);
        }
    }
}