using Isu.Entities;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly List<Group> _groups;
        
        public IsuService()
        {
            _groups = new List<Group>();
        }
        
        public Group AddGroup(string name)
        {
            if (_groups.Any(group => group.GroupName == name))
            {
                throw new GroupAlreadyExistsException($"The group {name} is already created!");
            }
            
            Group newGroup = new Group(name);
            _groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            Student newStudent = new Student(name, group);
            group.AddStudent(newStudent);
            return newStudent;
        }

        public Student GetStudent(int id)
        {
            var student = _groups.SelectMany(group => group.Students).SingleOrDefault(student => student.Id == id);
            if (student == null)
            {
                throw new NullReferenceException($"The student with id {id} doesn't exist!");
            }
            return student;
        }

        public Student? FindStudent(int id)
        {
            var student = _groups.SelectMany(group => group.Students).SingleOrDefault(student => student.Id == id);
            return student;
        }

        public IReadOnlyCollection<Student>? FindStudents(string groupName)
        {
            var groups = _groups.SingleOrDefault(group => group.GroupName == groupName);
            return groups?.Students;
        }

        public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
        {
            var groups = _groups.Where(group => group.CourseNumber.Number == courseNumber.Number).SelectMany(group => group.Students);
            return groups.ToList();
        }

        public Group? FindGroup(string groupName)
        {
            var group = _groups.SingleOrDefault(group => group.GroupName == groupName);
            return group;
        }

        public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber)
        {
            var groups = _groups.Where(group => group.CourseNumber.Number == courseNumber.Number);
            return groups.ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group oldGroup = student.Group;
            newGroup.AddStudent(student);
            oldGroup.RemoveStudent(student);
        }
    }
}