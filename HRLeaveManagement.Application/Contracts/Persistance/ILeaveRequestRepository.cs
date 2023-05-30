using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistance;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetails(int id);

    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();

    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);
}
