﻿using BetterBooks.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBooks.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }

        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            
            _db = db;
            Category = new CategoryRepository(_db);

            CoverType = new CoverTypeRepository(_db);//for cover type

            Product = new ProductRepository(_db);
        }
       
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
