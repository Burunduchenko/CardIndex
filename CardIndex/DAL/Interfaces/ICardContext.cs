using Microsoft.EntityFrameworkCore;

namespace DAL.Interfaces
{
    public interface ICardContext
    {
        int SaveChanges();
        DbSet<T> Set<T>() where T : class;
    }
}