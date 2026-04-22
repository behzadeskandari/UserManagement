using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Repositories.Interfaces
{
    public interface IUserRepository
    {


        Task<IEnumerable<User>> GetAllAsync(string search);

        Task CreateAsync(User user);

        Task UpdateAsync(User user);

        Task DeleteAsync(int id);
    }
}
