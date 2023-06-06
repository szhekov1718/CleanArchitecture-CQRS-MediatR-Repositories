using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserService _userService;
        private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _logger;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IUserService userService,
            IAppLogger<CreateLeaveAllocationCommandHandler> logger)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _userService = userService;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}!", nameof(Domain.LeaveAllocation), request.LeaveTypeId);

                throw new BadRequestException("Invalid Leave Allocation Request!", validationResult);
            }

            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            var employees = await _userService.GetEmployees();

            var period = DateTime.Now.Year;

            var allocations = new List<Domain.LeaveAllocation>();

            foreach (var emp in employees)
            {
                var allocationExists = await _leaveAllocationRepository.AllocationExists(emp.Id, request.LeaveTypeId, period);

                if (!allocationExists)
                {
                    allocations.Add(new Domain.LeaveAllocation
                    {
                        EmployeeId = emp.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period,
                    });

                    _logger.LogInformation("Leave allocation for EmployeeId {0} was successfully created!", emp.Id);
                }
            }

            if (allocations.Any())
            {
                await _leaveAllocationRepository.AddAllocations(allocations);
            }

            return Unit.Value;
        }
    }
}