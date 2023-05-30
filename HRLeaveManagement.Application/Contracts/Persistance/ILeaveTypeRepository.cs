using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistance;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsLeaveTypeUnique(string name);
}