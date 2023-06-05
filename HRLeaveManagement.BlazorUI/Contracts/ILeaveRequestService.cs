﻿using HRLeaveManagement.BlazorUI.Models.LeaveRequests;
using HRLeaveManagement.BlazorUI.Services.Base;

namespace HRLeaveManagement.BlazorUI.Contracts
{
    public interface ILeaveRequestService
    {
        Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList();

        Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests();

        Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest);

        Task<LeaveRequestVM> GetLeaveRequest(int id);

        Task DeleteLeaveRequest(int id);

        Task ApproveLeaveRequest(int id, bool approved);
    }
}
