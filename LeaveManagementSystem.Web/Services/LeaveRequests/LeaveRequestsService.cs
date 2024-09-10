using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveAllocations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveRequests
{
    public class LeaveRequestsService(IMapper _mapper, UserManager<ApplicationUser> _userManager,
        IHttpContextAccessor _httpContextAccessor,
        ApplicationDbContext _context) : ILeaveRequestsService
    {
        public Task CancelLeaveRequest(INestedHttpResult leaveRequestId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            //mapuj data iz vm u data model
            var leaveRequest = _mapper.Map<LeaveRequest>(model);

            //nadji logovanog usera
            var user=await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            leaveRequest.EmployeeId = user.Id;

            //stavi leaverequeststatus na pending
            leaveRequest.LeaveRequestStatusId=(int)LeaveRequestStatusEnum.Pending;

            //sacuvaj
            _context.LeaveRequests.Add(leaveRequest);

            //smanji broj dana u zavisnosti od zahteva
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocationToDeduct = await _context.LeaveAllocations
                .FirstOrDefaultAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == user.Id);
            allocationToDeduct.Days -= numberOfDays;

            await _context.SaveChangesAsync();
        }

        public Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequests()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            var leaveRequests = await _context.LeaveRequests
                .Include(q=>q.LeaveType)
                .Where(q => q.EmployeeId == user.Id)
                .ToListAsync();
            var model = leaveRequests.Select(q => new LeaveRequestReadOnlyVM
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveType = q.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber
            }).ToList();

            return model;

        }

        public Task ReviewLeaveRequest(ReviewListRequestVM model)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocation = await _context.LeaveAllocations
                .FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == user.Id);
            return allocation.Days <numberOfDays;
        }
    }
}
