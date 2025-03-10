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

            //For no tracking purpose
            //_db.ShoppingCarts.AsNoTracking().FirstOrDefault();
            //_db.ShoppingCarts.AsNoTracking().Where();





         //   _db.Products.Include(x => x.Category).Include(x => x.CoverType); //navigation property of category to be loaded and also we can use this to check wheather our include property in controller is Ok Or Not

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

       

        //includeProp - "Category,Covertype" & the filter to get all the products of one user
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter= null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
            
                query = query.Where(filter); //for shopping cart we are filtering
          
            }
           //for include prop
            if (includeProperties != null)
            {
                //if it having something split it
                foreach(var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)){

                    query = query.Include(includeProp);//one at a time

                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
        {

            IQueryable<T> query;

            if (tracked)
            {

                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
        
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
