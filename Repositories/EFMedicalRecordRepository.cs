using Microsoft.EntityFrameworkCore;
using QuanLyHSBA.Models;

namespace QuanLyHSBA.Repositories
{
    public class EFMedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly ApplicationDbContext _context;
        public EFMedicalRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MedicalRecord>> GetAllAsync()
        {
            return await _context.MedicalRecords.Include(p => p.Patient).Include(p => p.Doctor).ToListAsync();

        }
        public async Task<MedicalRecord> GetByIdAsync(int id)
        {
            return await _context.MedicalRecords.Include(p => p.Patient).Include(p => p.Doctor).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Update(medicalRecord);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            _context.MedicalRecords.Remove(medicalRecord);
            await _context.SaveChangesAsync();
        }
    }
}
