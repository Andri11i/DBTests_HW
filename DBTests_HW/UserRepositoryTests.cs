using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTests_HW
{
    using DBTests_HW.Data;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UserRepositoryTests : IDisposable
    {
        private readonly UserRepository _repository;
        private readonly DbContextOptions<UnittestsContext> _options;

        public UserRepositoryTests()
        {
            
            _options = new DbContextOptionsBuilder<UnittestsContext>()
                            .UseInMemoryDatabase("TestDb")
                            .Options;

          
            var context = new UnittestsContext(_options);
            _repository = new UserRepository(context);
        }

        [Fact]
        public void AddUser_AddsUserToDatabase()
        {
          
            var user = new User { Id = 1, Name = "Test User" };

         
            _repository.AddUser(user);

          
            using (var context = new UnittestsContext(_options))
            {
                Assert.NotNull(context.Users.Find(1));
            }
        }

        [Fact]
        public void UpdateUser_UpdatesUserInDatabase()
        {
          
            var user = new User { Id = 1, Name = "Original Name" };
            _repository.AddUser(user);
            user.Name = "Updated Name";

          
            _repository.UpdateUser(user);

         
            using (var context = new UnittestsContext(_options))
            {
                Assert.Equal("Updated Name", context.Users.Find(1).Name);
            }
        }

        [Fact]
        public void DeleteUser_RemovesUserFromDatabase()
        {
           
            var user = new User { Id = 1, Name = "Test User" };
            _repository.AddUser(user);

            _repository.DeleteUser(user.Id);

     
            using (var context = new UnittestsContext(_options))
            {
                Assert.Null(context.Users.Find(1));
            }
        }

        [Fact]
        public void GetUser_ReturnsUserFromDatabase()
        {
         
            var user = new User { Id = 1, Name = "Test User" };
            _repository.AddUser(user);

           
            var retrievedUser = _repository.GetUser(1);

          
            Assert.NotNull(retrievedUser);
            Assert.Equal("Test User", retrievedUser.Name);
        }

        [Fact]
        public void GetAllUsers_ReturnsAllUsers()
        {
            
            var user1 = new User { Id = 1, Name = "User One" };
            var user2 = new User { Id = 2, Name = "User Two" };
            _repository.AddUser(user1);
            _repository.AddUser(user2);

         
            var users = _repository.GetAllUsers();

          
            Assert.Equal(2, users.Count);
        }

        public void Dispose()
        {
          
            using (var context = new UnittestsContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }

}
