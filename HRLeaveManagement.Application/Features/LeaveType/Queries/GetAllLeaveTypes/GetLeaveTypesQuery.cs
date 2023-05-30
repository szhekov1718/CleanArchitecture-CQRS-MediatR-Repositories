using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    //public class GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>> {}

    public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
}
