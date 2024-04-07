using Microsoft.EntityFrameworkCore;
using QuanLyHSBA.Models;

namespace QuanLyHSBA.Repositories
{
    public class EFDoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        public EFDoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Doctor>> GetAllAsync()
        { 
        return await _context.Doctors.Include(p => p.Specialty).ToListAsync();

        }
        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.Include(p => p.Specialty).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
