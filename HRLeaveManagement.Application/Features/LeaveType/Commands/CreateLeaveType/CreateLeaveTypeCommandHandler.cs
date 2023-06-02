using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<CreateLeaveTypeCommandHandler> _logger;

        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<CreateLeaveTypeCommandHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors in update request for {0} - {1}!", nameof(Domain.LeaveType), request.Name);

                throw new BadRequestException("Invalid Leave Type!", validationResult);
            }

            var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

            await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

            _logger.LogInformation("Leave type with id {0} was successfully created!", leaveTypeToCreate.Id);

            return leaveTypeToCreate.Id;
        }
    }
}