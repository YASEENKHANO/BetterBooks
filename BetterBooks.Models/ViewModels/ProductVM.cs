using Microsoft.AspNetCore.Mvc;
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
      public Product Product = new Product(); //Product object

        //dropdown list for categories it will be populate from "unit of work" OR Projections using .Select()
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });


        IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
           u => new SelectListItem
           {
               Text = u.Name,
               Value = u.Id.ToString()
           });

    }
}
