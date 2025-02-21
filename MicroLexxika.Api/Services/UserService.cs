using MicroLexxika.Api.Models;
using MicroLexxika.Api.Services.Interfaces;
using MicroLexxika.Data;
using MicroLexxika.Domain.Models;
using MicroLexxika.Domain.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace MicroLexxika.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly HashingHelper _hashingHelper;
        public UserService(AppDbContext dbContext, HashingHelper hashingHelper)
        {
            _dbContext = dbContext;
            _hashingHelper = hashingHelper;
        }

        [HttpGet]
        public async Task<User?> GetAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            return user;
        }

        [HttpGet]
        public IList<User> GetAll()
        {
            var users = _dbContext.Users.ToList();

            return users;
        }

        [HttpPost]
        public User Create(UserRequest userRequest)
        {
            var user = new User
            {
                Username = userRequest.Username,
                Password = userRequest.Password,
                Role = (Role)userRequest.Role
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }
    }
}
