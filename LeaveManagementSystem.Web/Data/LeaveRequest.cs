namespace LeaveManagementSystem.Web.Data
{
    public class LeaveRequest:BaseEntity
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public LeaveType? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }

        public LeaveRequestStatus? LeaveRequestStatus { get; set; }
        public int LeaveRequestStatusId { get; set; }

        public ApplicationUser? Employee { get; set; }//onaj ko trazi request
        public string EmployeeId { get; set; } = default!;

        public ApplicationUser? Reviewer { get; set; }//onaj koji ce da ga odobri
        public string? ReviewerId { get; set; }//nullable je zato sto prvi put kad se uskaze zahtev
        //ovo mora da bude null, je ga jos nik nije review

        public string? RequestComments { get; set; }

    }
}