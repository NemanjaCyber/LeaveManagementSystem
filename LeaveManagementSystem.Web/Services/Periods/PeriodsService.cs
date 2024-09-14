using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.Periods
{
    public class PeriodsService(ApplicationDbContext _context):IPeriodsService
    {
        public async Task<Period> GetCurrentPeriod()
        {
            var currentData=DateTime.Now;
            var period=await _context.Periods.SingleAsync(q=>q.EndDate.Year==currentData.Year);
            return period;
        }
    }
}
