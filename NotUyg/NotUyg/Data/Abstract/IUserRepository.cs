using NotUyg.Entity;
using NotUyg.Models;

namespace NotUyg.Data.Abstract
{
    public interface IUserRepository
    {
        public IQueryable<User> User { get; }

        public Task create(User user);

    }
}
