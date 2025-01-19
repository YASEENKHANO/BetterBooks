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
           // _db.Products.Include(x => x.Category).Include(x => x.CoverType); //navigation property of category to be loaded

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

        //includeProp - "Category,Covertype"
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            //for include prop
            if(includeProperties != null)
            {
                //if it having something split it
                foreach(var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)){

                    query = query.Include(includeProp);//one at a time

                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {

            IQueryable<T> query = dbSet;



            query = query.Where(filter);

            if (includeProperties != null)
            {
                //if it having something split it
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(includeProp);//one at a time

                }
            }

            return query.FirstOrDefault();
        }
    }
}
