using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBooks.Models.ViewModels
{
    public class ProductVM
    {
        //public Product Product = new Product(); //Product object
        public Product Product { get; set; } = new Product();

        //dropdown list for categories it will be populate from "unit of work" OR Projections using .Select()
        [ValidateNever]
        public  IEnumerable<SelectListItem> CategoryList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }

    }
}
