using Microsoft.EntityFrameworkCore;
using QuanLyHSBA.Models;

namespace QuanLyHSBA.Repositories
{
    public class EFSpecialtyRepository : ISpecialtyRepository
    {
        private readonly ApplicationDbContext _context;
        public EFSpecialtyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Specialty>> GetAllAsync()
        {
            return await _context.Specialtys.ToListAsync();

        }
        public async Task<Specialty> GetByIdAsync(int id)
        {
            return await _context.Specialtys.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Specialty specialty)
        {
            _context.Specialtys.Add(specialty);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Specialty specialty)
        {
            _context.Specialtys.Update(specialty);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var specialty = await _context.Specialtys.FindAsync(id);
            _context.Specialtys.Remove(specialty);
            await _context.SaveChangesAsync();
        }
    }
}
