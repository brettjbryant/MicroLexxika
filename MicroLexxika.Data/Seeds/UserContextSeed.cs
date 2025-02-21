using MicroLexxika.Domain.Helpers;
using MicroLexxika.Domain.Models;

namespace MicroLexxika.Data
{
    public static class UserContextSeed
    {
        public static IList<User> Seed(AppDbContext dbContext, HashingHelper hashingHelper)
        {
            var users = new List<User>();

            if (!dbContext.Users.Any())
            {
                users.AddRange([
                    new User
                    {
                        Username = "admin1",
                        Password = hashingHelper.HashPassword("admin1"),
                        Role = Role.Admin
                    },
                    new User
                    {
                        Username = "admin2",
                        Password = hashingHelper.HashPassword("admin2"),
                        Role = Role.Admin
                    },
                    new User
                    {
                        Username = "user1",
                        Password = hashingHelper.HashPassword("user1"),
                        Role = Role.User
                    },
                    new User
                    {
                        Username = "user2",
                        Password = hashingHelper.HashPassword("user2"),
                        Role = Role.User
                    },
                    new User
                    {
                        Username = "user3",
                        Password = hashingHelper.HashPassword("user3"),
                        Role = Role.User
                    }]);

                dbContext.Users.AddRange(users);
                dbContext.SaveChanges();
            }

            return users;
        }
    }
}
