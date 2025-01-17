﻿using Isu.Entities;
using Isu.Models;

namespace Isu.Services
{
    public interface IIsuService
    {
        Group AddGroup(string name);
        Student AddStudent(Group group, string name);

        Student GetStudent(int id);
        Student? FindStudent(int id);
        IReadOnlyCollection<Student>? FindStudents(string groupName);
        IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber);

        Group? FindGroup(string groupName);
        IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber);

        void ChangeStudentGroup(Student student, Group newGroup);
    }
}