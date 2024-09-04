using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Data
{
    public class LeaveType : BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }
        public int NumberOfDays { get; set; }
    }
}
