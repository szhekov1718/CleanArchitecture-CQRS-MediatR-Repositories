using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _logger;

        public ChangeLeaveRequestApprovalCommandHandler(
             ILeaveRequestRepository leaveRequestRepository,
             ILeaveTypeRepository leaveTypeRepository,
             IMapper mapper,
             IEmailSender emailSender,
             IAppLogger<ChangeLeaveRequestApprovalCommandHandler> logger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null)
            {
                throw new EntityNotFoundException(nameof(Domain.LeaveRequest), request.Id);
            }

            leaveRequest.Approved = request.Approved;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            _logger.LogInformation("Leave request with id {0} approval status has been successfully updated!", leaveRequest.Id);

            // if request is approved, get and update the employee's allocations

            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated.",
                Subject = "Leave Request Approval Status Updated"
            };

            await _emailSender.SendEmail(email);

            return Unit.Value;
        }
    }
}
