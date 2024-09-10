using LeaveManagementSystem.Web.Models.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public interface ILeaveRequestsService
    {
        Task CreateLeaveRequest(LeaveRequestCreateVM model);
        Task<EmployeeLeaveRequestListVM> GetEmployeeLeaveRequests();//employee da vidi svoje leave requests
        Task<LeaveRequestListVM> GetAllLeaveRequests();//za admina da vidi sve leaverequests
        Task CancelLeaveRequest(INestedHttpResult leaveRequestId);
        Task ReviewLeaveRequest(ReviewListRequestVM model);
        Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model);
    }
}