using QuanLyHSBA.Models;

namespace QuanLyHSBA.Repositories
{
    public interface IMedicalRecordRepository
    {
        Task<IEnumerable<MedicalRecord>> GetAllAsync();
        Task<MedicalRecord> GetByIdAsync(int id);
        Task AddAsync(MedicalRecord medicalRecord);
        Task UpdateAsync(MedicalRecord medicalRecord);
        Task DeleteAsync(int id);
    }
}
