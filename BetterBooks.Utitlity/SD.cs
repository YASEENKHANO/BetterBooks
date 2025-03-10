using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBooks.Utitlity
{
    public static class SD
    {
        //SD means static details 



        //Customer side roles
        public const string Role_User_Indi = "Individual";
        public const string Role_User_Comp = "Company";
        //Company side user
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";



        //Status Properties
        public const string StatusPending = "Pending";

        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";


        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";


        //for session implemented in Home controller of customer area
        public const string SessionCart = "SessionShoppingCart";
    }
}
