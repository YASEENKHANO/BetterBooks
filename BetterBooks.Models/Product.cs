﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("List Price")]
        public double ListPrice { get; set; }

        //actual price
        [Required]
        [Range(1, 1000)]
      [DisplayName( "Price for 1-50")]
        public double Price { get; set; }

        //if buying more than 50
        [Required]
        [Range(1, 1000)]
        [DisplayName("Price for 51-100")]
        public double Price50 { get; set; }

        //if buying more than 100
        [Required]
        [Range(1, 1000)]
        [DisplayName("Price for 100+")]
        public double Price100 { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        //setting foreign key for category class
        [Required]
        [DisplayName("Category")]
        public int CategoryId  { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }


        //setting foreign key for CoverType class
        [Required]
        [DisplayName("Cover Type")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        public CoverType CoverType { get; set; }

    }
}
