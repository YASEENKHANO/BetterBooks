using BetterBooks.DataAccess.Repository.IRepository;
using BetterBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBooks.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeader.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
           var orderFromDB= _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            if (orderFromDB != null) { 
            
                orderFromDB.OrderStatus = orderStatus;
                if (paymentStatus != null) { 
                
                    orderFromDB.PaymentStatus = paymentStatus;
                
                }
            
            }
        }

        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDB = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            orderFromDB.PaymentDate= DateTime.Now;
            orderFromDB.SessionId = sessionId;
            orderFromDB.PaymentIntentId = paymentIntentId;
        }
    }
}
