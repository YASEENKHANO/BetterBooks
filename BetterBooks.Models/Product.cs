using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBooks.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        //for all products we will have list price
        [Required]
        [Range(1,1000)]
        public double ListPrice { get; set; }

        //actual price
        [Required]
        [Range(1, 1000)]
        public double Price { get; set; }

        //if buying more than 50
        [Required]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        //if buying more than 100
        [Required]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        public string ImageUrl { get; set; }

        //setting foreign key for category class
        [Required]
        
        public int CategoryId  { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }


        //setting foreign key for CoverType class
        [Required]
        public int CoverTypeId { get; set; }
        public CoverType CoverType { get; set; }

    }
}
