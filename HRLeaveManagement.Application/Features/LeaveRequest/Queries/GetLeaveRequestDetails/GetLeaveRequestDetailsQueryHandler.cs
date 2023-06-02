using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
    public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetLeaveRequestDetailsQueryHandler> _logger;

        public GetLeaveRequestDetailsQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IAppLogger<GetLeaveRequestDetailsQueryHandler> logger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveRequestDetails = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

            var leaveRequestDetailsDto = _mapper.Map<LeaveRequestDetailsDto>(leaveRequestDetails);

            if (leaveRequestDetailsDto == null)
            {
                throw new EntityNotFoundException(nameof(LeaveRequest), request.Id);
            }

            // Add Employee details as needed

            _logger.LogInformation("Leave request details were retrieved successfully!");

            return leaveRequestDetailsDto;
        }
    }
}
