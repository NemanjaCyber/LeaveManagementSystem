using LeaveManagementSystem.Web.Models.LeaveRequests;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public interface ILeaveRequestsService
    {
        Task CreateLeaveRequest(LeaveRequestCreateVM model);
        Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests();//employee da vidi svoje leave requests
        Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequests();//za admina da vidi sve leaverequests
        Task CancelLeaveRequest(INestedHttpResult leaveRequestId);
        Task ReviewLeaveRequest(ReviewListRequestVM model);
        Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model);
    }
}