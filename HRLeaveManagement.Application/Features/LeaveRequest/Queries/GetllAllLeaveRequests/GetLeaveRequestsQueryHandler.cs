using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetllAllLeaveRequests
{
    public class GetLeaveRequestQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetLeaveRequestQueryHandler> _logger;

        public GetLeaveRequestQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IAppLogger<GetLeaveRequestQueryHandler> logger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            // Check if employee is logged in

            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();

            var leaveRequestsDto = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            // Fill requests with employee information

            _logger.LogInformation("Leave requests were retrieved successfully!");

            return leaveRequestsDto;
        }
    }
}