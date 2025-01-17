using BetterBooks.DataAccess.Repository.IRepository;
using BetterBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBooks.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository( ApplicationDbContext db) : base(db )
        {
            _db=db;
        }

        public void Update(Product obj)
        {
           // _db.Products.Update(obj);//

            var objFromDB= _db.Products.FirstOrDefault(u=> u.Id==obj.Id);

            if (objFromDB != null) 
            { 
            
                objFromDB.Title = obj.Title;//title from new obj means new title
                objFromDB.ISBN = obj.ISBN;
                objFromDB.Description = obj.Description;
                objFromDB.Price = obj.Price;
                objFromDB.ListPrice = obj.ListPrice;
                objFromDB.Price50 = obj.Price50;
                objFromDB.Author = obj.Author;
                objFromDB.Price100 = obj.Price100;
                objFromDB.CategoryId= obj.CategoryId;
                objFromDB.CoverTypeId= obj.CoverTypeId;

                //cond for image, if user explicitly want to update image

                if (objFromDB.ImageUrl != null)
                {
                    objFromDB.ImageUrl= objFromDB.ImageUrl;
                }

            }
        }
    }
}
