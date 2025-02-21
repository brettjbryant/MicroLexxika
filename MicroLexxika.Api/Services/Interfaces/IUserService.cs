using MicroLexxika.Api.Models;
using MicroLexxika.Domain.Models;

namespace MicroLexxika.Api.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User?> GetAsync(int userId);
        public IList<User> GetAll();
        public User Create(UserRequest userRequest);
    }
}
