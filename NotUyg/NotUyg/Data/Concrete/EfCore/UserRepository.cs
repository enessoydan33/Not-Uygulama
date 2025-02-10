using NotUyg.Data.Abstract;
using NotUyg.Entity;
using NotUyg.Models;

namespace NotUyg.Data.Concrete.EfCore
{
    public class UserRepository : IUserRepository
    {
        public NotContext _context;

        public UserRepository(NotContext notContext)
        {
            _context = notContext;
        }
        public IQueryable<User> User => _context.User;



        public async Task create(User model)
        {

           await _context.User.AddAsync(model);
          await _context.SaveChangesAsync();
        }
    }
}


