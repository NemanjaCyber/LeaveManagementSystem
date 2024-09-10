using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    public class LeaveRequestCreateVM:IValidatableObject
    {
        [DisplayName("Start Date")]
        [Required]
        public DateOnly StartDate { get; set; }

        [DisplayName("End Date")]
        [Required]
        public DateOnly EndDate { get; set; }

        [DisplayName("Desired Leave Type")]
        [Required]
        public int LeaveTypeId { get; set; }

        [DisplayName("Additional Information")]
        [StringLength(250)]
        public string? RequestComments { get; set; }

        public SelectList? LeaveTypes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
            {
                yield return new ValidationResult("The start date must be before end date",
                [
                    nameof(StartDate), nameof(EndDate)
                ]);
            }
        }
    }
}