using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Respositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ICardContext _cardDbContext;
        private readonly DbSet<User> _users;

        public UserRepository(ICardContext cardDbContext)
        {
            _cardDbContext = cardDbContext;
            _users = _cardDbContext.Set<User>();
        }

        public async Task<User> AddAsync(User item)
        {
            await _users.AddAsync(item);
            _cardDbContext.SaveChanges();
            return item;
        }

        public void DeleteById(int id)
        {
            var result = _users.Find(id);
            _users.Remove(result);
            _cardDbContext.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            var users = _users;
            return users;
        }

        public IEnumerable<User> GetAllWithDetails()
        {
            var users = _users.Include(x => x.ArticleRates);
            return users;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _users.FindAsync(id);
            return user;
        }

        public async Task<User> GetByIdWithDetaileAsync(int id)
        {
            var users = _users.Include(x => x.ArticleRates);
            var user = await users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public User Update(User item)
        {
            var element = _users.Where(x => x.Id == item.Id).First();
            element.Email = item.Email;
            element.FirstName = item.FirstName;
            element.LastName = item.LastName;
            element.Password = item.Password;
            element.Login = item.Login;
            element.PhoneNumber = item.PhoneNumber;

            _cardDbContext.SaveChanges();
            return item;
        }
    }
}
