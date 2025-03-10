using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBooks.Models;

namespace BetterBooks.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader> //not bracekts you fool
    {

        void Update(OrderHeader obj);

      
        void UpdateStatus(int id, string orderStatus, string? paymentStatus=null);
        void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId);
    } 
}
