using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BetterBooks.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class //Interface only has calling 
    {
        //This is A Generic repository
        // T suppose for now is Category

        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();

        void Add(T entity);
       // void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);



    }
}
