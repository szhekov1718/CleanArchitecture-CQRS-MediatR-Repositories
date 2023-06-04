using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Domain;
using Moq;

namespace HRLeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 15,
                    Name = "Sick"
                },
                new LeaveType
                {
                    Id = 3,
                    DefaultDays = 15,
                    Name = "Мaternity"
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();

            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });

            return mockRepo;
        }
    }
}
