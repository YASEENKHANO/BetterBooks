﻿using Microsoft.EntityFrameworkCore;
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

        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked= true);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);

        void Add(T entity);
       // void Update(T entity);
        void Delete(T entity);

        public void Remove(T entity);
       



    }
}
