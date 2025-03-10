using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBooks.Models;

namespace BetterBooks.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart> //not bracekts you fool
    {
        public int IncrementCount(ShoppingCart shoppingCart, int count);
        public int DecrementCount(ShoppingCart shoppingCart, int count);


        void RemoveRange(IEnumerable<ShoppingCart> shoppingCarts); // New method for removing multiple items


    }
}
