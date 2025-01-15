using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBooks.Models
{
    internal class ShoppingCart
    {
      //  public Property Property { get; set; }
        
        
        [Range(1,1000,ErrorMessage ="")]
        public int Count { get; set; }
    }
}
