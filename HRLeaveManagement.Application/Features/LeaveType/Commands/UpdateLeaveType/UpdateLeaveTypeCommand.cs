using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommand : IRequest<Unit> // use unit, because we expect to return nothing since it's an update command
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int DefaultDays { get; set; }
    }
}