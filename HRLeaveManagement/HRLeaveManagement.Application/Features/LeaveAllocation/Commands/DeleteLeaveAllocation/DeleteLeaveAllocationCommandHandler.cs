using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IAppLogger<DeleteLeaveAllocationCommandHandler> _logger;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, IAppLogger<DeleteLeaveAllocationCommandHandler> logger)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

            if (leaveAllocation == null)
            {
                throw new EntityNotFoundException(nameof(Domain.LeaveAllocation), request.Id);
            }

            await _leaveAllocationRepository.DeleteAsync(leaveAllocation);

            _logger.LogInformation("Leave allocation was successfully deleted!");

            return Unit.Value;
        }
    }
}