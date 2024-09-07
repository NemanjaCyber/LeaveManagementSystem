
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationsService(ApplicationDbContext _context,
        IHttpContextAccessor _httpContextAccessor
        , UserManager<ApplicationUser> _userManager
        , IMapper _mapper) : ILeaveAllocationsService
    {
        public async Task AllocateLeave(string employeeId)
        {
            var leaveTypes = await _context.LeaveTypes
                .Where(q => !q.LeaveAllocations.Any(x => x.EmployeeId == employeeId))
                .ToListAsync();

            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            var monthsReamining = period.EndDate.Month - currentDate.Month;

            foreach (var leaveType in leaveTypes)
            {
                //var allocationExists=await AllocationExists(employeeId, period.Id, leaveType.Id);
                //if(allocationExists)
                //{
                //    continue;
                //}    
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

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId)
        {
            var user = string.IsNullOrEmpty(userId)
                ? await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User)
                : await _userManager.FindByIdAsync(userId);
            var allocations = await GetAllocations(user.Id);
            var allocationVMList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
            var leaveTypesCount = await _context.LeaveTypes.CountAsync();
            var employeeVM = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                LeaveAllocations = allocationVMList,
                IsCompletedAllocation = leaveTypesCount == allocations.Count
            };

            return employeeVM;
        }

        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
            var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

            return employees;
        }

       

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int? id)
        {
            var allocation = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(q => q.Id == id);

            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);
            return model;
        }

        public async Task EditAllocation(LeaveAllocationEditVM allocationEditVM)
        {
            //opcija 1 - dva pristupa bazi. ceo record se update
            //var leaveAllocation=await GetEmployeeAllocation(allocationEditVM.Id);
            //if(leaveAllocation==null)
            //{
            //    throw new Exception("Leave allocation record does not exist.");
            //}

            //leaveAllocation.Days = allocationEditVM.Days;
            //_context.Update(leaveAllocation);
            //await _context.SaveChangesAsync();

            //opcija 2 - jedan pristup bazi. Samo se update ono sto sami napisemo(ovde samo days)
            await _context.LeaveAllocations
                .Where(q => q.Id == allocationEditVM.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, allocationEditVM.Days));
        }


        private async Task<bool> AllocationExists(string userId, int periodId, int leaveTypeId)
        {
            var exists = await _context.LeaveAllocations
                .AnyAsync(q => q.EmployeeId == userId
                && q.PeriodId == periodId
                && q.LeaveTypeId == leaveTypeId);

            return exists;
        }

        private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
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

            var leaveAllocations = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Period)
                .Where(q => q.EmployeeId == userId && q.Period.EndDate.Year == currentDate.Year)
                .ToListAsync();

            return leaveAllocations;
        }
    }

}
        

