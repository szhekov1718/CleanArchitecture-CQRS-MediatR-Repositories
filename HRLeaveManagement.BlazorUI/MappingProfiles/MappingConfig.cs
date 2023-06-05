using AutoMapper;
using HRLeaveManagement.BlazorUI.Models.LeaveTypes;

namespace HRLeaveManagement.BlazorUI.MappingProfiles
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeDetailsDto, LeaveTypeVM>().ReverseMap();
            CreateMap<CreateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
            CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();

            CreateMap<LeaveRequestListDto, LeaveRequestVM>().ReverseMap();
            CreateMap<LeaveRequestDetailsDto, LeaveRequestVM>().ReverseMap();
            CreateMap<CreateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();

            CreateMap<LeaveAllocationDto, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocationDetailsDto, LeaveAllocationVM>().ReverseMap();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();
        }
    }
}
