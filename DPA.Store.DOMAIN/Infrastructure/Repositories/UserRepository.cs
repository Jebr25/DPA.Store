using DPA.Store.DOMAIN.Core.Entities;
using DPA.Store.DOMAIN.Core.Interfaces;
using DPA.Store.DOMAIN.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA.Store.DOMAIN.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly StoreDbContext _dbContext;

        public UserRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _dbContext.User.FirstOrDefaultAsync(t => t.Email == email);
        }

        public async Task<bool> Insert(User user)
        {
            await _dbContext.User.AddAsync(user);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            var user = await FindByEmail(email);

            if (user != null)
            {
                return user.Password == password;
            }

            return false;
        }

        public async Task<bool> DeleteById(int id)
        {
            var user = await _dbContext.User.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (user == null)
                return false;

            _dbContext.User.Remove(user);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
