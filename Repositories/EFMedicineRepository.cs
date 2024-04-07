using Microsoft.EntityFrameworkCore;
using QuanLyHSBA.Models;

namespace QuanLyHSBA.Repositories
{
    public class EFMedicineRepository : IMedicineRepository
    {
        private readonly ApplicationDbContext _context;
        public EFMedicineRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Medicine>> GetAllAsync()
        {
            return await _context.Medicines.ToListAsync();

        }
        public async Task<Medicine> GetByIdAsync(int id)
        {
            return await _context.Medicines.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Medicine medicine)
        {
            _context.Medicines.Update(medicine);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
        }
    }
}
