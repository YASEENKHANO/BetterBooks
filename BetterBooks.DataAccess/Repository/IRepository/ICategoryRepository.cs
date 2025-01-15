using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBooks.Models;

namespace BetterBooks.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category> //not bracekts you fool
    {

        void Update(Category obj);

        //void Save();
    } 
}
