using QuanLyHSBA.Models;

namespace QuanLyHSBA.Repositories
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetAllAsync();
        Task<Specialty> GetByIdAsync(int id);
        Task AddAsync(Specialty specialty);
        Task UpdateAsync(Specialty specialty);
        Task DeleteAsync(int id);
    }
}
