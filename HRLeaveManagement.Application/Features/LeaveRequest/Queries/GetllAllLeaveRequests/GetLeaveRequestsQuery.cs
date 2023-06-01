using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetllAllLeaveRequests
{
    public record GetLeaveRequestsQuery : IRequest<List<LeaveRequestDto>>;
}
