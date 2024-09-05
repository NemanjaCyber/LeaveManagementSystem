
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationsService(ApplicationDbContext _context, 
        IHttpContextAccessor _httpContextAccessor
        ,UserManager<ApplicationUser> _userManager
        ,IMapper _mapper) : ILeaveAllocationsService
    {
        public async Task AllocateLeave(string employeeId)
        {
            var leaveTypes = await _context.LeaveTypes.ToListAsync();

            var currentDate=DateTime.Now;
            var period = await _context.Periods.SingleAsync(q=>q.EndDate.Year==currentDate.Year);
            var monthsReamining=period.EndDate.Month-currentDate.Month;

            foreach(var leaveType in leaveTypes)
            {
                var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    PeriodId = period.Id,
                    Days = (int)Math.Ceiling(accuralRate * monthsReamining)
                };

                _context.Add(leaveAllocation);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<LeaveAllocation>> GetAllocations()
        {//user je loged in user

            var currentDate = DateTime.Now;

            //opcija 1
            //var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            //var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            //var leaveAllocations = await _context.LeaveAllocations
            //    .Include(q => q.LeaveType)
            //    .Include(q => q.Period)
            //    .Where(q => q.EmployeeId == user.Id && q.PeriodId == period.Id)
            //    .ToListAsync();

            //opcija 2
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            var leaveAllocations = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Period)
                .Where(q => q.EmployeeId == user.Id && q.Period.EndDate.Year == currentDate.Year)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations()
        {
            var allocations = await GetAllocations();
            var allocationVMList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            var employeeVM = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                LeaveAllocations = allocationVMList
            };

            return employeeVM;
        }
    }
}
