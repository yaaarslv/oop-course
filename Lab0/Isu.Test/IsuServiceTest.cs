using Isu.Services;
using Isu.Entities;
using Isu.Models;
using Isu.Tools;
using Xunit;

namespace Isu.Tests
{
    public class IsuServiceTest
    {
        IsuService service = new IsuService();
        
        [Fact]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group m32051 = service.AddGroup("M32051");
            Student testStudent = service.AddStudent(m32051, "Testov Test Testovich");
            Assert.True(testStudent.Group.GroupName == "M32051");
            Assert.Contains(testStudent, m32051.Students);
        }

        [Fact]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Throws<GroupIsFullException>( ()=> {
                Group m32051 = service.AddGroup("M32051");
                for (int i = 0; i < Group.GroupCapacity + 1; i++) 
                {
                    service.AddStudent(m32051, "Testov Test Testovich"); 
                } 
            });
        }

        [Fact]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Throws<InvalidGroupNameException>(() =>
            {
                service.AddGroup("K320q1133");
            });
        }
        
        [Fact]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group oldGroup = service.AddGroup("M3105");
            Group newGroup = service.AddGroup("M32051");
            Student testStudent = service.AddStudent(oldGroup, "Testov Test Testovich");
            Assert.True(testStudent.Group.GroupName == "M3105");
            Assert.Contains(testStudent, oldGroup.Students);
            service.ChangeStudentGroup(testStudent, newGroup);
            Assert.True(testStudent.Group.GroupName == "M32051");
            Assert.Contains(testStudent, newGroup.Students);
        }
    }
}