using BetterBooks.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BetterBooks.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class // This is a generic repository class that implements the IRepository interface
    {
        private readonly ApplicationDbContext _db; //dependency injection 
        internal DbSet<T> dbSet;// before we were using Category 
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet= _db.Set<T>();//implementing solid repository
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity); // Removes the entity from the DbSet, meaning it's marked for deletion

        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {

            IQueryable<T> query = dbSet;

            query = query.Where(filter);

             
            return query.FirstOrDefault();
        }
    }
}
