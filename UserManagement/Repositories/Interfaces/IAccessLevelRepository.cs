using UserManagement.Models;

namespace UserManagement.Repositories.Interfaces
{
    public interface IAccessLevelRepository
    {
        Task<IEnumerable<AccessLevel>> GetAllAsync();
        Task<AccessLevel?> GetByIdAsync(int id);
        Task CreateAsync(AccessLevel accessLevel);
        Task UpdateAsync(AccessLevel accessLevel);
    }
}
