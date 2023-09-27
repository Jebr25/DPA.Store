using DPA.Store.DOMAIN.Core.Entities;

namespace DPA.Store.DOMAIN.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindByEmail(string email);
        Task<bool> Insert(User user);
        Task<bool> ValidateUser(string email, string password);
        Task<bool> DeleteById(int id);
    }
}