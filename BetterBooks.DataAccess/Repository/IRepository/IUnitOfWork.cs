﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBooks.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; } //implementing all the repos here
        ICoverTypeRepository CoverType { get; } //implementing all the repos here
        IProductRepository Product { get; } //implementing all the repos here
        ICompanyRepository Company { get; } //implementing all the repos here
        IShoppingCartRepository ShoppingCart { get; } //implementing all the repos here
        IApplicationUserRepository ApplicationUser { get; } //implementing all the repos here
        IOrderDetailRepository OrderDetail { get; } //implementing all the repos here
        IOrderHeaderRepository OrderHeader { get; } //implementing all the repos here


        void Save(); //method which is needed global
        
    }
}
