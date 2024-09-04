
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationsService(ApplicationDbContext _context) : ILeaveAllocationsService
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
    }
}
