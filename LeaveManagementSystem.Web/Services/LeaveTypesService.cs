using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services
{
    public class LeaveTypesService(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypesService//primary constructor
    {
        //fora sa servisom je da se smanji kod u controlleru, ova logika treba da bude u servisu, koji cina interfejs i klasa.
        public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
        {
            var data = await _context.LeaveTypes.ToListAsync();
            //convert datamodel to a viewmodel ILI use AutoMapper
            //var viewData = data.Select(q => new IndexVM
            //{
            //    Id = q.Id,
            //    Name = q.Name,
            //    NumberOfDays = q.NumberOfDays,
            //});
            var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);//ovaj mapper nam automatski radi ovo sto je iznad zakomentarisano
            //return a viewmodel to a view
            return viewData;
        }

        public async Task<T?> Get<T>(int id) where T : class
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (data == null)
            {
                return null;
            }
            var viewData = _mapper.Map<T>(data);
            return viewData;
        }

        public async Task Remove(int id)
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Edit(LeaveTypeEditVM model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);
            _context.Update(leaveType);
            await _context.SaveChangesAsync();
        }

        public async Task Create(LeaveTypeCreateVM model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);
            _context.Add(leaveType);
            await _context.SaveChangesAsync();
        }

        public bool LeaveTypeExists(int id)
        {
            return _context.LeaveTypes.Any(e => e.Id == id);
        }
        public async Task<bool> CheckIfLeaveTypeNameExists(string name)
        {
            var lowerCaseName = name.ToLower();
            return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName));
        }
        public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit)
        {
            var lowerCaseName = leaveTypeEdit.Name.ToLower();
            return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName) &&
            q.Id != leaveTypeEdit.Id);
        }
    }
}
