using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBooks.Models;

namespace BetterBooks.DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail> //not bracekts you fool
    {

        void Update(OrderDetail obj);

        //void Save();
    } 
}
