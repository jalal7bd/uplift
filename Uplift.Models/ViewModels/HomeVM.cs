using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models.ViewModels
{
   public class HomeVM
    {
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
    }
}
