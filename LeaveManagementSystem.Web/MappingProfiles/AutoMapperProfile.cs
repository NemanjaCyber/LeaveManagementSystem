using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            CreateMap<LeaveTypeCreateVM,LeaveType>();//iz LeavetypeCreateVM u LeaveType, zato sto se ovaj prvi koristi za formu kroz koju se
            //kreira nov LeaveType, pa mora da se konvertuje u LeaveType, tj. u onaj koji se pamti u bazi podataka
            CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();//treba nam preslikavanje u oba smera
        }
    }
}
