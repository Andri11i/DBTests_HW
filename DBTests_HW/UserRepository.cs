using DBTests_HW.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTests_HW
{
    public class UserRepository
    {
        private readonly UnittestsContext _context;

        public UserRepository(UnittestsContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User GetUser(int userId)
        {
            return _context.Users.Find(userId);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }

}
