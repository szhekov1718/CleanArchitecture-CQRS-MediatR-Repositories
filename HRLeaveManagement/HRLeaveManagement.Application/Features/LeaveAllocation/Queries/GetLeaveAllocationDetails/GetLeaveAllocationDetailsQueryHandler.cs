using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQueryHandler : IRequestHandler<GetLeaveAllocationDetailQuery, LeaveAllocationDetailsDto>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetLeaveAllocationDetailsQueryHandler> _logger;

        public GetLeaveAllocationDetailsQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, IAppLogger<GetLeaveAllocationDetailsQueryHandler> logger)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }
        public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

            if (leaveAllocation == null)
            {
                throw new EntityNotFoundException(nameof(Domain.LeaveAllocation), request.Id);
            }

            var leaveAllocationDetailsDto = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);

            _logger.LogInformation("Leave allocation details were retrieved successfully!");

            return leaveAllocationDetailsDto;
        }
    }
}
