using Microsoft.AspNetCore.Identity;

namespace LeaveManagementSystem.Web.Data
{
    public class ApplicationUser : IdentityUser//dodajemo ovoj klasi ApplicationUser koja nam je data iz Identity da joj dodamo nove propertije
    {//u DbContrext smo morali da dodamo i IdentityDbContext<ApplicationUser>, ovo u dijamant, i u program.cs builder.Services.AddDefaultIdentity<ApplicationUser>
        //takodje, promene u seeding. Iz ApplicationUser u ApplicationUser
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
