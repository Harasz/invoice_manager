using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using invoice_manager.DBContexts;
using invoice_manager.Dtos;
using invoice_manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MySqlConnector;

namespace invoice_manager.Services
{
    public class UserService
    {
        private readonly MainDbContext _dbContext;

        public UserService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static List<GetUser> ToDto(IEnumerable<User> users)
        {
            return users.Select(ToDto).ToList();
        }

        public static GetUser ToDto(User user)
        {
            return new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
        
        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.Users.Where(user => user.Email == email).FirstAsync();
        }

        public async Task<EntityEntry<User>> Create(PutUser user)
        {
            try
            {
                var result = await _dbContext.Users.AddAsync(
                    new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Role = UserRoles.User,
                        PasswordHash = HashPassword(user.Password)
                    });
                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is MySqlException {ErrorCode: MySqlErrorCode.DuplicateKeyEntry}) return null;

                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var user = await GetById(id);

            if (user is null) return false;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> MakeAdmin(User user)
        {
            user.Role = UserRoles.Admin;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private static string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            var hash = pbkdf2.GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string passwordHash)
        {
            var hashBytes = Convert.FromBase64String(passwordHash);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            var hash = pbkdf2.GetBytes(20);

            for (ushort i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i]) return false;
            }
            return true;
        }

        public Task<int> GetOverallUserByRole(UserRoles role)
        {
            return _dbContext.Users.Where(user => user.Role == role).CountAsync();
        }
    }
}